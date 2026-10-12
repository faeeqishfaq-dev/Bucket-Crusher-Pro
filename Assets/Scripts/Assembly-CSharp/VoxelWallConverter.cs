using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class VoxelWallConverter : MonoBehaviour
{
    // VISUAL DATA
    private struct BlockVisual
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public int materialIndex;
        public Color color;
    }

    

    private static readonly int ColorPropertyID = Shader.PropertyToID("_Color");
    private Texture2D currentTexture;
    private System.Collections.Generic.Dictionary<Transform, BlockVisual> visualCache = new System.Collections.Generic.Dictionary<Transform, BlockVisual>();
        private System.Collections.Generic.Queue<GameObject> brickPool = new System.Collections.Generic.Queue<GameObject>();
    private System.Collections.Generic.Queue<GameObject> fakeBrickPool = new System.Collections.Generic.Queue<GameObject>();

    public void ReturnBrickToPool(GameObject brick)
    {
        if (brick != null)
        {
            brick.SetActive(false);
            brickPool.Enqueue(brick);
        }
    }

    public void ReturnFakeBrickToPool(GameObject brick)
    {
        if (brick != null && brick.activeSelf && !fakeBrickPool.Contains(brick))
        {
            brick.SetActive(false);
            fakeBrickPool.Enqueue(brick);
        }
    }

    public static VoxelWallConverter Instance;

    [Header("REFERENCE STYLE - BLOCK SIZE")]
    [SerializeField] private Vector3 baseBlockSize = new Vector3(0.18f, 0.11f, 0.11f);

    [Header("IRREGULAR BRICK LOOK")]
    [SerializeField] private float sizeVariationX = 0.03f;
    [SerializeField] private float sizeVariationY = 0.02f;

    [SerializeField] private float positionJitterX = 0.002f;
    [SerializeField] private float positionJitterY = 0.0005f;
    [SerializeField] private float depthJitter = 0.018f;

    [SerializeField] private float rotationJitterX = 2f;
    [SerializeField] private float rotationJitterY = 3f;
    [SerializeField] private float rotationJitterZ = 5f;

    [Header("BRICK COLOR")]
    [SerializeField]
    private Color brickColor =
        new Color32(202, 112, 95, 255);

    [SerializeField, Range(0f, 0.25f)]
    private float colorVariation = 0.08f;

    [Header("BROKEN BLOCK PHYSICS")]
    [SerializeField] private float brokenBlockMass = 0.10f;
    [SerializeField] private float outwardForce = 0.45f;
    [SerializeField] private float randomForce = 0.20f;
    [SerializeField] private float torque = 2.5f;
    [SerializeField] private float brokenBlockLife = 20f; // Increased to allow accumulation

    
    private Mesh cubeMesh;
    private SkinnedMeshRenderer smr;

    public int ActiveBonesCount { get; private set; }

    private readonly List<Transform> activeBones =
        new List<Transform>();

    private bool isDirty;

    // Several slightly different brick colors.
    private const int MATERIAL_VARIANTS = 5;
    private Material[] brickMaterials;

    private class DrawBatch
    {
        public int materialIndex;
        public Matrix4x4[] matrices;
        public Vector4[] colors;
        public int count;
    }

    private readonly List<DrawBatch> drawBatches =
        new List<DrawBatch>();

    // --------------------------------------------------
    // VISUAL DATA

    // --------------------------------------------------

    

    private void Awake()
    {
        Instance = this;
        // Layer 10 (WallBones) and Layer 11 (FallingDebris)
        // Ignored to prevent explosive collision upon spawn
        Physics.IgnoreLayerCollision(10, 11, true);
        Physics.IgnoreLayerCollision(6, 11, true);
    }

    private void Start()
    {
        FindSourceRenderer();
        CreateCubeMesh();
        CreateBrickMaterials();

        Map map = GetComponent<Map>();

        if (map == null)
            map = Object.FindAnyObjectByType<Map>();

        if (map == null)
        {
            Debug.LogError(
                "[VoxelWallConverter] Map component not found."
            );

            return;
        }

        if (map.Blocks == null || map.Blocks.Count == 0)
            map.Processs();

        activeBones.Clear();

        if (map.Blocks != null)
        {
            activeBones.AddRange(map.Blocks);
            ActiveBonesCount = activeBones.Count;
            
            // Set all static bones to Layer 10
            visualCache.Clear();
        foreach (Transform bone in activeBones)
        {
            if (bone != null)
            {
                bone.gameObject.layer = 10;
                visualCache[bone] = GetBlockVisual(bone);
            }
        }
        }

        // Safety: only disable if we actually built the matrices
        // We will disable it in Update() if successful.
        // if (smr != null) smr.enabled = false;

        CreateIntakeTrigger();
        CreateIntakeChute();

        RebuildMatrices();
    }

    private void CreateIntakeTrigger()
    {
        GameObject excavator = GameObject.Find("excavator");
        if (excavator == null) return;

        GameObject trigger = new GameObject("DebrisIntakeTrigger");
        trigger.transform.SetParent(excavator.transform, false);
        // TOP INTAKE / HOPPER opening: local to excavator
        trigger.transform.localPosition = new Vector3(-1.369f, 1.8f, 0f);
        
        BoxCollider box = trigger.AddComponent<BoxCollider>();
        box.isTrigger = true;
        // Small trigger ONLY at the actual intake/hopper opening
        box.size = new Vector3(0.8f, 0.5f, 2.0f);
        
        trigger.AddComponent<DebrisIntake>();
        
        MachineIntakeMagnet magnet = trigger.AddComponent<MachineIntakeMagnet>();
        
        GameObject p1 = new GameObject("IntakeEntryPoint");
        p1.transform.SetParent(excavator.transform, false);
        p1.transform.localPosition = new Vector3(-0.3f, 2.2f, 0f);

        GameObject p2 = new GameObject("IntakeSlopePoint");
        p2.transform.SetParent(excavator.transform, false);
        p2.transform.localPosition = new Vector3(-0.9f, 1.8f, 0f);

        GameObject p3 = new GameObject("IntakeConsumePoint");
        p3.transform.SetParent(excavator.transform, false);
        p3.transform.localPosition = new Vector3(-1.369f, 1.4f, 0f);

        magnet.intakeEntryPoint = p1.transform;
        magnet.intakeSlopePoint = p2.transform;
        magnet.intakeConsumePoint = p3.transform;
    }
    private void CreateIntakeChute()
    {
        GameObject excavator = GameObject.Find("excavator");
        if (excavator == null) return;

        // Fix Cube (3) blocking the intake
        Transform hopper = excavator.transform.Find("Top/Cube (3)");
        if (hopper != null)
        {
            Collider hopperCol = hopper.GetComponent<Collider>();
            if (hopperCol != null) hopperCol.isTrigger = true; // DO NOT block debris physically!
        }

    }


    // --------------------------------------------------
    // SOURCE WALL
    // --------------------------------------------------

    private void FindSourceRenderer()
    {
        if (transform.parent != null)
        {
            smr =
                transform.parent.GetComponentInChildren
                <SkinnedMeshRenderer>(true);
        }

        if (smr == null)
        {
            smr =
                Object.FindAnyObjectByType
                <SkinnedMeshRenderer>();
        }
    }

    // --------------------------------------------------
    // CUBE MESH
    // --------------------------------------------------

    private void CreateCubeMesh()
    {
        GameObject temp =
            GameObject.CreatePrimitive(PrimitiveType.Cube);

        cubeMesh =
            temp.GetComponent<MeshFilter>().sharedMesh;

        Destroy(temp);
    }

    // --------------------------------------------------
    // CLEAN BRICK MATERIAL
    // --------------------------------------------------

    private void CreateBrickMaterials()
    {
        Shader shader = Shader.Find("BucketCrusher/OptimizedBricks");
        if (shader == null && smr != null && smr.sharedMaterial != null)
        {
            shader = smr.sharedMaterial.shader;
        }
        if (shader == null)
        {
            shader = Shader.Find("Standard");
        }

        brickMaterials = new Material[1];
        brickMaterials[0] = new Material(shader);
        brickMaterials[0].enableInstancing = true;
    }

    private void EnsureLevelTexture()
    {
        if (currentTexture == null)
        {
            MapCreatorEditor mce = transform.parent != null ? transform.parent.GetComponent<MapCreatorEditor>() : UnityEngine.Object.FindAnyObjectByType<MapCreatorEditor>();
            if (mce != null && mce.CurrentLevel != null)
            {
                currentTexture = mce.CurrentLevel.Sprite;
            }
        }
    }

    // --------------------------------------------------
    // DETERMINISTIC RANDOM BLOCK APPEARANCE
    // --------------------------------------------------

    private BlockVisual GetBlockVisual(Transform bone)
    {
        EnsureLevelTexture();

        System.Random rng = new System.Random(bone.GetInstanceID());
        float Range(float min, float max) { return Mathf.Lerp(min, max, (float)rng.NextDouble()); }

        Vector3 positionOffset = new Vector3(Range(-positionJitterX, positionJitterX), Range(-positionJitterY, positionJitterY), Range(-depthJitter, depthJitter));
        Quaternion rotationOffset = Quaternion.Euler(Range(-rotationJitterX, rotationJitterX), Range(-rotationJitterY, rotationJitterY), Range(-rotationJitterZ, rotationJitterZ));
        float sx = Range(1f - sizeVariationX, 1f + sizeVariationX);
        float sy = Range(1f - sizeVariationY, 1f + sizeVariationY);
        Vector3 finalScale = new Vector3(baseBlockSize.x * sx, baseBlockSize.y * sy, baseBlockSize.z);

        BlockVisual result = new BlockVisual();
        result.position = bone.position + positionOffset;
        result.rotation = bone.rotation * rotationOffset;
        result.scale = finalScale;
        result.materialIndex = 0;
        
        result.color = Color.white;
        if (currentTexture != null)
        {
            string[] parts = bone.name.Split('.');
            if (parts.Length >= 3 && int.TryParse(parts[1], out int px) && int.TryParse(parts[2], out int py))
            {
                px = Mathf.Clamp(px, 0, currentTexture.width - 1);
                py = Mathf.Clamp(py, 0, currentTexture.height - 1);
                Color c = currentTexture.GetPixel(px, py);
                result.color = new Color(c.r, c.g, c.b, 1f);
            }
            else
            {
                float spacingX = 0.077f / 100f;
                float spacingY = 0.055f / 100f;
                int step = 7;
                float totalW = ((float)currentTexture.width / step) * spacingX;
                float totalH = ((float)currentTexture.height / step) * spacingY;
                float u = totalW > 0.001f ? Mathf.Clamp01(-bone.localPosition.x / totalW) : 0.5f;
                float v = totalH > 0.001f ? Mathf.Clamp01(bone.localPosition.y / totalH) : 0.5f;
                int posPx = Mathf.Clamp(Mathf.RoundToInt(u * (currentTexture.width - 1)), 0, currentTexture.width - 1);
                int posPy = Mathf.Clamp(Mathf.RoundToInt(v * (currentTexture.height - 1)), 0, currentTexture.height - 1);
                Color c = currentTexture.GetPixel(posPx, posPy);
                result.color = new Color(c.r, c.g, c.b, 1f);
            }
        }
        
        return result;
    }

    // --------------------------------------------------
    // BUILD GPU INSTANCED WALL
    // --------------------------------------------------

        private System.Collections.Generic.List<Matrix4x4>[] groups;
    private System.Collections.Generic.List<Vector4>[] groupsColors;
    private readonly System.Collections.Generic.List<DrawBatch> batchPool = new System.Collections.Generic.List<DrawBatch>();

    private void RebuildMatrices()
    {
        drawBatches.Clear();
        if (brickMaterials == null) return;

        if (groups == null)
        {
            groups = new System.Collections.Generic.List<Matrix4x4>[MATERIAL_VARIANTS];
            groupsColors = new System.Collections.Generic.List<Vector4>[MATERIAL_VARIANTS];
            for (int i = 0; i < MATERIAL_VARIANTS; i++)
            {
                groups[i] = new System.Collections.Generic.List<Matrix4x4>(4096);
                groupsColors[i] = new System.Collections.Generic.List<Vector4>(4096);
            }
        }

        for (int i = 0; i < MATERIAL_VARIANTS; i++)
        {
            groups[i].Clear();
            groupsColors[i].Clear();
        }

        for (int i = 0; i < activeBones.Count; i++)
        {
            Transform bone = activeBones[i];
            if (bone == null) continue;
            if (bone.localScale.sqrMagnitude < 0.01f) continue;

            BlockVisual visual;
            if (!visualCache.TryGetValue(bone, out visual))
                visual = GetBlockVisual(bone);

            Matrix4x4 matrix = Matrix4x4.TRS(visual.position, visual.rotation, visual.scale);
            groups[visual.materialIndex].Add(matrix);
            groupsColors[visual.materialIndex].Add(visual.color);
        }

        int poolIndex = 0;
        for (int material = 0; material < MATERIAL_VARIANTS; material++)
        {
            System.Collections.Generic.List<Matrix4x4> source = groups[material];
            System.Collections.Generic.List<Vector4> sourceColors = groupsColors[material];

            int offset = 0;
            while (offset < source.Count)
            {
                int count = Mathf.Min(1023, source.Count - offset);
                
                DrawBatch batch;
                if (poolIndex < batchPool.Count)
                {
                    batch = batchPool[poolIndex];
                }
                else
                {
                    batch = new DrawBatch();
                    batch.matrices = new Matrix4x4[1023];
                    batch.colors = new Vector4[1023];
                    batchPool.Add(batch);
                }
                poolIndex++;

                source.CopyTo(offset, batch.matrices, 0, count);
                sourceColors.CopyTo(offset, batch.colors, 0, count);

                batch.materialIndex = material;
                batch.count = count;

                drawBatches.Add(batch);
                offset += count;
            }
        }
    }
    // --------------------------------------------------
    // DRAW
    // --------------------------------------------------

    
        private MaterialPropertyBlock propBlock;
    private void Update()
    {
        if (isDirty)
        {
            RebuildMatrices();
            isDirty = false;
        }

        if (activeBones.Count > 0 && drawBatches.Count > 0)
        {
            if (smr != null && smr.enabled) smr.enabled = false;
        }

        if (cubeMesh == null || brickMaterials == null) return;

        if (propBlock == null) propBlock = new MaterialPropertyBlock();

        for (int i = 0; i < drawBatches.Count; i++)
        {
            DrawBatch batch = drawBatches[i];
            Material material = brickMaterials[batch.materialIndex];
            if (material == null) continue;

            propBlock.Clear();
            if (batch.colors != null)
                propBlock.SetVectorArray(ColorPropertyID, batch.colors);

            Graphics.DrawMeshInstanced(
                cubeMesh, 0, material, batch.matrices, batch.count,
                propBlock, UnityEngine.Rendering.ShadowCastingMode.On, true, gameObject.layer
            );
        }
    }

    public void ResetBlocks()
    {
        Map map = GetComponent<Map>();
        if (map == null) map = UnityEngine.Object.FindAnyObjectByType<Map>();
        if (map == null || map.Blocks == null) return;

        FindSourceRenderer();
        if (cubeMesh == null) CreateCubeMesh();
        CreateBrickMaterials();

        MapCreatorEditor mce = transform.parent != null ? transform.parent.GetComponent<MapCreatorEditor>() : UnityEngine.Object.FindAnyObjectByType<MapCreatorEditor>();
        if (mce != null && mce.CurrentLevel != null)
        {
            currentTexture = mce.CurrentLevel.Sprite;
        }

        activeBones.Clear();
        activeBones.AddRange(map.Blocks);
        ActiveBonesCount = activeBones.Count;
        
        visualCache.Clear();
        foreach (Transform bone in activeBones)
        {
            if (bone != null)
            {
                bone.gameObject.layer = 10;
                visualCache[bone] = GetBlockVisual(bone);
            }
        }
        isDirty = true;
    }

        public void OnBoneCrushed(Transform hitTransform)
    {
        Transform bone = FindActualWallBone(hitTransform);
        if (bone == null) return;

        BlockVisual visual;
        if (!visualCache.TryGetValue(bone, out visual))
            visual = GetBlockVisual(bone);

        visualCache.Remove(bone);
        ActiveBonesCount--;

        isDirty = true;

        bool isFake = (bone.GetSiblingIndex() % 2 == 0);
        SpawnFallingBrick(visual, isFake);
    }

    private Transform FindActualWallBone(Transform hit)
    {
        if (visualCache.ContainsKey(hit)) return hit;
        
        Transform current = hit.parent;
        while (current != null)
        {
            if (visualCache.ContainsKey(current)) return current;
            current = current.parent;
        }
        return null;
    }

    // --------------------------------------------------
    // FALLING REAL PHYSICS BLOCK
    // --------------------------------------------------

    private void SpawnFallingBrick(BlockVisual visual, bool isFake)
    {
        if (isFake)
        {
            GameObject block;
            MeshRenderer renderer;
            FakeDebrisMovement fakeMove;
            
            if (fakeBrickPool.Count > 0)
            {
                block = fakeBrickPool.Dequeue();
                block.SetActive(true);
                renderer = block.GetComponent<MeshRenderer>();
                fakeMove = block.GetComponent<FakeDebrisMovement>();
            }
            else
            {
                block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                block.name = "FakeBrokenWallBrick";
                block.layer = 14; // Unused layer, completely ignored by physics/intake triggers!
                
                // No Rigidbody, no colliders!
                Destroy(block.GetComponent<BoxCollider>());
                
                fakeMove = block.AddComponent<FakeDebrisMovement>();
                renderer = block.GetComponent<MeshRenderer>();
            }

            block.transform.position = visual.position;
            block.transform.rotation = visual.rotation;
            block.transform.localScale = visual.scale;

            if (renderer != null && brickMaterials != null)
            {
                renderer.sharedMaterial = brickMaterials[visual.materialIndex];
                MaterialPropertyBlock prop = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(prop);
                prop.SetColor(ColorPropertyID, visual.color);
                renderer.SetPropertyBlock(prop);
            }

            fakeMove.Init(visual.position, this);
            return;
        }

        {
            GameObject block;
            Rigidbody rb;
            MeshRenderer renderer;
            
            if (brickPool.Count > 0)
            {
                block = brickPool.Dequeue();
                block.SetActive(true);
                rb = block.GetComponent<Rigidbody>();
                renderer = block.GetComponent<MeshRenderer>();
                
                rb.isKinematic = true; // reset forces
            }
        else
        {
            block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.name = "BrokenWallBrick";
            block.layer = 11;
            
            BoxCollider collider = block.GetComponent<BoxCollider>();
            PhysicsMaterial lowFriction = new PhysicsMaterial("LowFrictionDebris");
            lowFriction.dynamicFriction = 0.1f;
            lowFriction.staticFriction = 0.1f;
            lowFriction.bounciness = 0.1f;
            lowFriction.frictionCombine = PhysicsMaterialCombine.Minimum;
            if (collider != null) collider.material = lowFriction;
            
            rb = block.AddComponent<Rigidbody>();
            rb.mass = brokenBlockMass;
            rb.interpolation = RigidbodyInterpolation.None; // Optimize: Interp is too heavy for 1000s of debris
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete; // Optimize: Continuous is insanely heavy
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            
            renderer = block.GetComponent<MeshRenderer>();
        }

        block.transform.position = visual.position;
        block.transform.rotation = visual.rotation;
        block.transform.localScale = visual.scale;

        if (renderer != null && brickMaterials != null)
        {
            renderer.sharedMaterial = brickMaterials[visual.materialIndex];
            MaterialPropertyBlock prop = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(prop);
            prop.SetColor(ColorPropertyID, visual.color);
            renderer.SetPropertyBlock(prop);
        }

        rb.useGravity = true;
        rb.isKinematic = false;
        
        rb.linearVelocity = new Vector3(
            UnityEngine.Random.Range(-0.08f, 0.08f),
            UnityEngine.Random.Range(-0.08f, 0.02f),
            UnityEngine.Random.Range(-0.03f, 0.03f)
        );
        rb.angularVelocity = UnityEngine.Random.insideUnitSphere * 1.5f;
        }
    }

    private void OnDestroy()
    {
        if (brickMaterials == null)
            return;

        for (int i = 0;
             i < brickMaterials.Length;
             i++)
        {
            if (brickMaterials[i] != null)
                Destroy(
                    brickMaterials[i]);
        }
    }
}