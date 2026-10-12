import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/MachineIntakeMagnet.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """using UnityEngine;
using System.Collections.Generic;

public class MachineIntakeMagnet : MonoBehaviour
{
    [Header("Intake Path Points")]
    public Transform intakeEntryPoint;
    public Transform intakeSlopePoint;
    public Transform intakeConsumePoint;

    [Header("Magnet Settings")]
    public bool magnetEnabled = true;
    public float attractionRadius = 2.5f;
    
    public float entryPullStrength = 10f;
    public float slopePullStrength = 15f;
    public float finalIntakeStrength = 20f;
    
    public float maxIntakeSpeed = 5f;
    public float damping = 4f;
    public float consumeDistance = 0.2f;

    [Header("Soft Retention System")]
    public bool retentionEnabled = true;
    public float maxDebrisDistance = 6.0f;
    public float returnForce = 5f;
    public float maxReturnSpeed = 3f;

    // Track which stage each cube is in
    private Dictionary<Rigidbody, int> cubeStages = new Dictionary<Rigidbody, int>();
    private Dictionary<Rigidbody, float> cubeStuckTimers = new Dictionary<Rigidbody, float>();

    // OPTIMIZATIONS
    private Collider[] _overlapBuffer = new Collider[150];
    private Collider[] _retentionBuffer = new Collider[150];
    private HashSet<Rigidbody> _currentRigidbodies = new HashSet<Rigidbody>();
    private List<Rigidbody> _keysToRemove = new List<Rigidbody>();

    private void FixedUpdate()
    {
        if (!magnetEnabled) return;
        if (intakeEntryPoint == null || intakeSlopePoint == null || intakeConsumePoint == null) return;

        _currentRigidbodies.Clear();
        _keysToRemove.Clear();

        // Use the entry point as the center of attraction detection (NonAlloc)
        int count = Physics.OverlapSphereNonAlloc(intakeEntryPoint.position, attractionRadius, _overlapBuffer);

        for (int i = 0; i < count; i++)
        {
            Collider col = _overlapBuffer[i];
            if (col.gameObject.layer == 11)
            {
                Rigidbody rb = col.attachedRigidbody;
                if (rb != null)
                {
                    _currentRigidbodies.Add(rb);
                    ProcessCube(rb);
                }
            }
        }

        // Soft Retention System
        if (retentionEnabled && intakeEntryPoint != null)
        {
            // Replaced extremely slow FindObjectsOfType with a larger OverlapSphereNonAlloc
            int retCount = Physics.OverlapSphereNonAlloc(intakeEntryPoint.position, maxDebrisDistance + 2f, _retentionBuffer);
            for (int i = 0; i < retCount; i++)
            {
                Collider col = _retentionBuffer[i];
                if (col.gameObject.layer == 11)
                {
                    Rigidbody debris = col.attachedRigidbody;
                    if (debris != null)
                    {
                        Vector3 center = intakeEntryPoint.position;
                        Vector3 direction = center - debris.position;
                        float distance = direction.magnitude;

                        // If cube is outside normal collection area
                        if (distance > maxDebrisDistance)
                        {
                            // Ensure it's active
                            debris.isKinematic = false;
                            if (debris.IsSleeping()) debris.WakeUp();

                            Vector3 dirNorm = direction.normalized;
                            Vector3 targetVelocity = dirNorm * maxReturnSpeed;
                            float lerpFactor = returnForce * Time.fixedDeltaTime;
                            lerpFactor = Mathf.Clamp01(lerpFactor);

                            // Smoothly steer velocity back toward machine
                            debris.AddForce((targetVelocity - debris.linearVelocity) * (lerpFactor * 10f), ForceMode.Acceleration);
                        }
                    }
                }
            }
        }

        // Cleanup dictionary for cubes that were consumed or left the area (Zero GC)
        foreach (var kvp in cubeStages)
        {
            if (kvp.Key == null || !_currentRigidbodies.Contains(kvp.Key))
            {
                _keysToRemove.Add(kvp.Key);
            }
        }
        
        for (int i = 0; i < _keysToRemove.Count; i++)
        {
            Rigidbody rb = _keysToRemove[i];
            cubeStages.Remove(rb);
            cubeStuckTimers.Remove(rb);
        }
    }

    private void ProcessCube(Rigidbody rb)
    {
        if (!cubeStages.ContainsKey(rb))
        {
            if (rb.position.x < intakeSlopePoint.position.x)
                cubeStages[rb] = 3;
            else if (rb.position.x < intakeEntryPoint.position.x)
                cubeStages[rb] = 2;
            else
                cubeStages[rb] = 1;
        }

        int stage = cubeStages[rb];
        Vector3 targetPos = Vector3.zero;
        float pullStrength = 0f;

        if (stage == 1)
        {
            targetPos = intakeEntryPoint.position;
            pullStrength = entryPullStrength;
            
            if (Vector3.Distance(rb.position, targetPos) < 0.4f)
            {
                cubeStages[rb] = 2;
                targetPos = intakeSlopePoint.position;
                pullStrength = slopePullStrength;
            }
        }
        else if (stage == 2)
        {
            targetPos = intakeSlopePoint.position;
            pullStrength = slopePullStrength;
            
            if (Vector3.Distance(rb.position, targetPos) < 0.3f)
            {
                cubeStages[rb] = 3;
                targetPos = intakeConsumePoint.position;
                pullStrength = finalIntakeStrength;
            }
        }
        else if (stage == 3)
        {
            targetPos = intakeConsumePoint.position;
            pullStrength = finalIntakeStrength;
        }

        Vector3 direction = targetPos - rb.position;
        float distance = direction.magnitude;

        if (distance > 0.05f)
        {
            Vector3 dirNorm = direction.normalized;

            Vector3 targetVelocity = dirNorm * maxIntakeSpeed;
            float lerpFactor = damping * pullStrength * Time.fixedDeltaTime;
            lerpFactor = Mathf.Clamp01(lerpFactor); 

            rb.AddForce((targetVelocity - rb.linearVelocity) * (lerpFactor * 10f), ForceMode.Acceleration);
        }

        if (rb.linearVelocity.sqrMagnitude < 0.04f) // optimized magnitude
        {
            if (!cubeStuckTimers.ContainsKey(rb)) cubeStuckTimers[rb] = 0f;
            cubeStuckTimers[rb] += Time.fixedDeltaTime;

            if (cubeStuckTimers[rb] > 0.35f)
            {
                rb.WakeUp();
                Vector3 chuteDir = (targetPos - rb.position).normalized;
                chuteDir.y += 0.5f; 
                rb.AddForce(chuteDir.normalized * 0.4f, ForceMode.Impulse);
                cubeStuckTimers[rb] = 0f;
            }
        }
        else
        {
            cubeStuckTimers[rb] = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (intakeEntryPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(intakeEntryPoint.position, attractionRadius);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(intakeEntryPoint.position, intakeSlopePoint.position);
            Gizmos.DrawLine(intakeSlopePoint.position, intakeConsumePoint.position);
        }
    }
}"""

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(optimized)

print("Optimized MachineIntakeMagnet.cs")
