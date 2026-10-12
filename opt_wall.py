import re

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Add Pool and Cache variables
pattern_vars = r'public float brokenBlockLife = 3f;\n'
replacement_vars = '''public float brokenBlockLife = 3f;

    // POOLING & CACHING FOR PERFORMANCE
    private System.Collections.Generic.Dictionary<Transform, BlockVisual> visualCache = new System.Collections.Generic.Dictionary<Transform, BlockVisual>();
    private System.Collections.Generic.Queue<GameObject> brickPool = new System.Collections.Generic.Queue<GameObject>();

    public void ReturnBrickToPool(GameObject brick)
    {
        brick.SetActive(false);
        brickPool.Enqueue(brick);
    }
'''
content = re.sub(pattern_vars, replacement_vars, content)

# Populate cache in ResetBlocks
pattern_reset = r'foreach \(Transform bone in activeBones\)\s*\{\s*if \(bone != null\)\s*bone\.gameObject\.layer = 10;\s*\}'
replacement_reset = '''visualCache.Clear();
        foreach (Transform bone in activeBones)
        {
            if (bone != null)
            {
                bone.gameObject.layer = 10;
                visualCache[bone] = GetBlockVisual(bone);
            }
        }'''
content = re.sub(pattern_reset, replacement_reset, content)

# Use cache in RebuildMatrices
pattern_rebuild = r'BlockVisual visual =\s*GetBlockVisual\(bone\);'
replacement_rebuild = '''BlockVisual visual;
            if (!visualCache.TryGetValue(bone, out visual))
                visual = GetBlockVisual(bone);'''
content = re.sub(pattern_rebuild, replacement_rebuild, content)

# Optimize SpawnFallingBrick with Pooling
pattern_spawn = r'private void SpawnFallingBrick\(BlockVisual visual\)\s*\{.*?\s*// Destroy\(block, brokenBlockLife\); // Wall cubes never auto-destroy\n    \}'
replacement_spawn = """private void SpawnFallingBrick(BlockVisual visual)
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
            PhysicMaterial lowFriction = new PhysicMaterial("LowFrictionDebris");
            lowFriction.dynamicFriction = 0.1f;
            lowFriction.staticFriction = 0.1f;
            lowFriction.bounciness = 0.1f;
            lowFriction.frictionCombine = PhysicMaterialCombine.Minimum;
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
        }

        rb.useGravity = true;
        rb.isKinematic = false;
        
        rb.linearVelocity = new Vector3(
            UnityEngine.Random.Range(-0.08f, 0.08f),
            UnityEngine.Random.Range(-0.08f, 0.02f),
            UnityEngine.Random.Range(-0.03f, 0.03f)
        );
        rb.angularVelocity = UnityEngine.Random.insideUnitSphere * 1.5f;
    }"""
content = re.sub(pattern_spawn, replacement_spawn, content, flags=re.DOTALL)

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'w', encoding='utf-8') as f:
    f.write(content)
