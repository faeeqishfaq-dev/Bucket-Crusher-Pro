import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/DebrisIntake.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebrisIntake : MonoBehaviour
{
    private Material goldMat;
    private Material silverMat;
    
    private Transform coinOutputPoint;
    private VoxelWallConverter _cachedConverter;

    // POOLING FOR COINS
    private Queue<GameObject> coinPool = new Queue<GameObject>();
    private Dictionary<GameObject, Rigidbody> coinRigidbodies = new Dictionary<GameObject, Rigidbody>();
    private Dictionary<GameObject, MeshRenderer> coinRenderers = new Dictionary<GameObject, MeshRenderer>();
    private Dictionary<GameObject, CoinData> coinDatas = new Dictionary<GameObject, CoinData>();

    private void Start()
    {
        _cachedConverter = Object.FindAnyObjectByType<VoxelWallConverter>();

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

        GameObject pitFloor = new GameObject("InvisiblePitFloor");
        if (parentTransform != null) pitFloor.transform.SetParent(parentTransform, false);
        pitFloor.transform.localPosition = new Vector3(-1.382f, -2.5f, 0f);
        BoxCollider solidBox = pitFloor.AddComponent<BoxCollider>();
        solidBox.size = new Vector3(3.0f, 1.0f, 2.0f);
        
        BoxCollider triggerBox = pitFloor.AddComponent<BoxCollider>();
        triggerBox.size = new Vector3(3.0f, 3.0f, 2.0f);
        triggerBox.center = new Vector3(0f, 1.0f, 0f);
        triggerBox.isTrigger = true;
        
        pitFloor.AddComponent<CoinTankTrigger>();
        
        GameObject leftWall = new GameObject("InvisiblePitLeft");
        if (parentTransform != null) leftWall.transform.SetParent(parentTransform, false);
        leftWall.transform.localPosition = new Vector3(-2.882f, -1.0f, 0f);
        BoxCollider leftBox = leftWall.AddComponent<BoxCollider>();
        leftBox.size = new Vector3(1.0f, 4.0f, 2.0f);

        GameObject rightWall = new GameObject("InvisiblePitRight");
        if (parentTransform != null) rightWall.transform.SetParent(parentTransform, false);
        rightWall.transform.localPosition = new Vector3(0.118f, -1.0f, 0f);
        BoxCollider rightBox = rightWall.AddComponent<BoxCollider>();
        rightBox.size = new Vector3(1.0f, 4.0f, 2.0f);

        GameObject output = new GameObject("CoinOutputPoint");
        if (parentTransform != null) output.transform.SetParent(parentTransform, false);
        output.transform.localPosition = new Vector3(-1.382f, -0.1f, 0f);
        coinOutputPoint = output.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (_cachedConverter != null)
            {
                _cachedConverter.ReturnBrickToPool(other.gameObject);
            }
            else
            {
                other.gameObject.SetActive(false);
            }
            
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        GameObject coin;
        Rigidbody rb;
        MeshRenderer mr;
        CoinData data;
        
        if (coinPool.Count > 0)
        {
            coin = coinPool.Dequeue();
            coin.SetActive(true);
            rb = coinRigidbodies[coin];
            mr = coinRenderers[coin];
            data = coinDatas[coin];
            rb.isKinematic = true;
        }
        else
        {
            coin = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            coin.name = "CollectedCoin";
            coin.transform.localScale = new Vector3(0.08f, 0.015f, 0.08f);
            
            Destroy(coin.GetComponent<CapsuleCollider>());
            SphereCollider sphere = coin.AddComponent<SphereCollider>();
            sphere.radius = 0.5f;

            rb = coin.AddComponent<Rigidbody>();
            rb.mass = 0.05f;
            rb.useGravity = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.interpolation = RigidbodyInterpolation.None;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            mr = coin.GetComponent<MeshRenderer>();
            data = coin.GetComponent<CoinData>();

            coinRigidbodies[coin] = rb;
            coinRenderers[coin] = mr;
            coinDatas[coin] = data;
        }

        coin.transform.position = coinOutputPoint.position + new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), 0f);
        coin.transform.rotation = Quaternion.Euler(90f, Random.Range(0f, 360f), 0f);
        
        mr.sharedMaterial = Random.value > 0.3f ? goldMat : silverMat;
        
        if (data != null) data.collected = false;

        rb.isKinematic = false;
        rb.linearVelocity = new Vector3(Random.Range(-0.2f, 0.2f), -1f, 0f);
        
        StartCoroutine(ReturnCoinLater(coin));
    }
    
    private IEnumerator ReturnCoinLater(GameObject coin)
    {
        yield return new WaitForSeconds(5f);
        if (coin != null && coin.activeSelf)
        {
            coin.SetActive(false);
            coinPool.Enqueue(coin);
        }
    }
}
"""

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(optimized)

print("Optimized DebrisIntake.cs")
