using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebrisIntake : MonoBehaviour
{
    [Header("Coin Storage Tank")]
    [SerializeField] private Transform coinStorage;
    [SerializeField] private Vector3 storageSize = new Vector3(4.5f, 4.0f, 3.0f); // Enlarged moderate size (original: 3, 3, 2)
    [SerializeField] private Vector3 storageOffset = new Vector3(-1.382f, -2.5f, 0f);
    [SerializeField] private BoxCollider storageCollider;

    private BoxCollider floorCollider;
    private GameObject leftWallObj;
    private GameObject rightWallObj;
    private BoxCollider leftWallCol;
    private BoxCollider rightWallCol;

    private Material goldMat;
    private Material silverMat;
    private Transform coinOutputPoint;

    // POOLING FOR COINS
    private Queue<GameObject> coinPool = new Queue<GameObject>();

    private void Start()
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null)
            shader = Shader.Find("Standard");

        // Setup coin materials
        goldMat = new Material(shader);
        Color goldColor = new Color(1.0f, 0.8f, 0.0f);
        if (goldMat.HasProperty("_BaseColor")) goldMat.SetColor("_BaseColor", goldColor);
        if (goldMat.HasProperty("_Color")) goldMat.SetColor("_Color", goldColor);
        goldMat.color = goldColor;
        
        silverMat = new Material(shader);
        Color silverColor = new Color(0.75f, 0.75f, 0.75f);
        if (silverMat.HasProperty("_BaseColor")) silverMat.SetColor("_BaseColor", silverColor);
        if (silverMat.HasProperty("_Color")) silverMat.SetColor("_Color", silverColor);
        silverMat.color = silverColor;

        GameObject excavator = GameObject.Find("excavator");
        Transform parentTransform = excavator != null ? excavator.transform : null;

        // Ensure there is a floor for the pit so coins accumulate
        if (coinStorage == null)
        {
            GameObject pitFloor = new GameObject("InvisiblePitFloor");
            coinStorage = pitFloor.transform;
            if (parentTransform != null) pitFloor.transform.SetParent(parentTransform, false);
            
            floorCollider = pitFloor.AddComponent<BoxCollider>();
            
            storageCollider = pitFloor.AddComponent<BoxCollider>();
            storageCollider.isTrigger = true;
            
            pitFloor.AddComponent<CoinTankTrigger>();
        }
        else
        {
            floorCollider = coinStorage.GetComponent<BoxCollider>();
            if (floorCollider == null) floorCollider = coinStorage.gameObject.AddComponent<BoxCollider>();

            BoxCollider[] cols = coinStorage.GetComponents<BoxCollider>();
            foreach (var col in cols)
            {
                if (col.isTrigger) storageCollider = col;
            }
            if (storageCollider == null)
            {
                storageCollider = coinStorage.gameObject.AddComponent<BoxCollider>();
                storageCollider.isTrigger = true;
            }
            
            if (coinStorage.GetComponent<CoinTankTrigger>() == null)
            {
                coinStorage.gameObject.AddComponent<CoinTankTrigger>();
            }
        }
        
        // Also add side walls so coins don't spill out
        leftWallObj = new GameObject("InvisiblePitLeft");
        if (parentTransform != null) leftWallObj.transform.SetParent(parentTransform, false);
        leftWallCol = leftWallObj.AddComponent<BoxCollider>();

        rightWallObj = new GameObject("InvisiblePitRight");
        if (parentTransform != null) rightWallObj.transform.SetParent(parentTransform, false);
        rightWallCol = rightWallObj.AddComponent<BoxCollider>();

        // Dedicated bottom outlet for coins
        GameObject output = new GameObject("CoinOutputPoint");
        if (parentTransform != null) output.transform.SetParent(parentTransform, false);
        output.transform.localPosition = new Vector3(-1.382f, -0.1f, 0f);
        coinOutputPoint = output.transform;

        // Apply initial sizing
        UpdateStorageSize();
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            UpdateStorageSize();
        }
    }

    private void UpdateStorageSize()
    {
        if (coinStorage != null && floorCollider != null && leftWallObj != null && rightWallObj != null && storageCollider != null)
        {
            coinStorage.localPosition = storageOffset;
            
            // Floor
            floorCollider.size = new Vector3(storageSize.x, 1.0f, storageSize.z);
            
            // Trigger volume
            storageCollider.size = new Vector3(storageSize.x, storageSize.y, storageSize.z);
            // Height offset adjusted to keep bottom aligned with the floor
            storageCollider.center = new Vector3(0f, 1.0f + (storageSize.y - 3.0f)/2f, 0f);
            
            // Walls (placed at the edges of the width)
            leftWallObj.transform.localPosition = storageOffset + new Vector3(-storageSize.x / 2f, 1.5f, 0f);
            leftWallCol.size = new Vector3(1.0f, storageSize.y + 1f, storageSize.z);
            
            rightWallObj.transform.localPosition = storageOffset + new Vector3(storageSize.x / 2f, 1.5f, 0f);
            rightWallCol.size = new Vector3(1.0f, storageSize.y + 1f, storageSize.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            // Return to pool instead of destroying
            VoxelWallConverter converter = Object.FindAnyObjectByType<VoxelWallConverter>();
            if (converter != null)
            {
                converter.ReturnBrickToPool(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }
            
            // Convert to coin from bottom outlet
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        GameObject coin;
        Rigidbody rb;
        
        if (coinPool.Count > 0)
        {
            coin = coinPool.Dequeue();
            coin.SetActive(true);
            rb = coin.GetComponent<Rigidbody>();
            rb.isKinematic = true; // reset
        }
        else
        {
            coin = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            coin.name = "CollectedCoin";
            
            // Make it look like a flat coin/token
            coin.transform.localScale = new Vector3(0.08f, 0.015f, 0.08f);
            
            Destroy(coin.GetComponent<CapsuleCollider>());
            SphereCollider sphere = coin.AddComponent<SphereCollider>();
            sphere.radius = 0.5f;

            rb = coin.AddComponent<Rigidbody>();
            rb.mass = 0.05f;
            rb.useGravity = true;
            
            // OPTIMIZED PHYSICS
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.interpolation = RigidbodyInterpolation.None;
            
            rb.constraints = 
                RigidbodyConstraints.FreezePositionZ | 
                RigidbodyConstraints.FreezeRotationX | 
                RigidbodyConstraints.FreezeRotationY;
        }

        coin.transform.position = coinOutputPoint.position + new Vector3(
            Random.Range(-0.02f, 0.02f), 
            Random.Range(-0.02f, 0.02f), 
            0f
        );
        coin.transform.rotation = Quaternion.Euler(90f, Random.Range(0f, 360f), 0f);
        
        MeshRenderer mr = coin.GetComponent<MeshRenderer>();
        mr.sharedMaterial = Random.value > 0.3f ? goldMat : silverMat;
        
        // Reset collected state
        CoinData data = coin.GetComponent<CoinData>();
        if (data != null) data.collected = false;

        rb.isKinematic = false;
        rb.linearVelocity = new Vector3(Random.Range(-0.2f, 0.2f), -1f, 0f);
        
        // Return to pool after 5 seconds
        StartCoroutine(ReturnCoinLater(coin));
    }
    
    private IEnumerator ReturnCoinLater(GameObject coin)
    {
        yield return new WaitForSeconds(5f);
        if (coin != null)
        {
            coin.SetActive(false);
            coinPool.Enqueue(coin);
        }
    }
}
