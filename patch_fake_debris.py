import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/FakeDebrisMovement.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """using UnityEngine;

public class FakeDebrisMovement : MonoBehaviour
{
    private static Transform entry;
    private static Transform slope;
    private static Transform consume;
    private static float attractionRadius = 2.5f;
    private static bool initializedStatic = false;

    private int stage = 1;
    private VoxelWallConverter converter;
    private Vector3 velocity;

    public void Init(Vector3 startPos, VoxelWallConverter conv)
    {
        transform.position = startPos;
        converter = conv;
        stage = 1;

        if (!initializedStatic)
        {
            var magnet = Object.FindAnyObjectByType<MachineIntakeMagnet>();
            if (magnet != null)
            {
                entry = magnet.intakeEntryPoint;
                slope = magnet.intakeSlopePoint;
                consume = magnet.intakeConsumePoint;
                attractionRadius = magnet.attractionRadius;
                initializedStatic = true;
            }
        }
        
        velocity = new Vector3(
            Random.Range(-3.0f, 3.0f),
            Random.Range(0f, 2.0f),
            Random.Range(-1.0f, 1.0f)
        );
    }

    private void Update()
    {
        if (entry == null)
        {
            velocity.y -= 9.81f * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
            if (transform.position.y < -10f)
            {
                if (converter != null) converter.ReturnFakeBrickToPool(gameObject);
                else gameObject.SetActive(false);
            }
            return;
        }
        
        Vector3 targetPos = Vector3.zero;
        if (stage == 1)
        {
            targetPos = entry.position;
            if (Vector3.Distance(transform.position, targetPos) < 0.4f) stage = 2;
        }
        else if (stage == 2)
        {
            targetPos = slope.position;
            if (Vector3.Distance(transform.position, targetPos) < 0.3f) stage = 3;
        }
        else if (stage == 3)
        {
            targetPos = consume.position;
            if (Vector3.Distance(transform.position, targetPos) < 0.2f)
            {
                if (converter != null) converter.ReturnFakeBrickToPool(gameObject);
                else gameObject.SetActive(false);
                return;
            }
        }

        Vector3 direction = targetPos - transform.position;
        float distance = direction.magnitude;

        if (distance > attractionRadius + 0.5f)
        {
            velocity.y -= 9.81f * Time.deltaTime;
        }
        else
        {
            if (distance > 0.05f)
            {
                Vector3 dirNorm = direction.normalized;
                float pullSpeed = stage == 1 ? 7f : 12f;
                velocity = Vector3.Lerp(velocity, dirNorm * pullSpeed, Time.deltaTime * 6f);
            }
        }
        
        transform.position += velocity * Time.deltaTime;
        transform.Rotate(velocity * (Time.deltaTime * 45f), Space.World);
    }
}
"""

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(optimized)

print("Optimized FakeDebrisMovement.cs")
