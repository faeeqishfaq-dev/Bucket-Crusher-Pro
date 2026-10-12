using UnityEngine;
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

    private void FixedUpdate()
    {
        if (!magnetEnabled) return;
        if (intakeEntryPoint == null || intakeSlopePoint == null || intakeConsumePoint == null) return;

        // Use the entry point as the center of attraction detection
        Collider[] colliders = Physics.OverlapSphere(intakeEntryPoint.position, attractionRadius);

        HashSet<Rigidbody> currentRigidbodies = new HashSet<Rigidbody>();

        foreach (Collider col in colliders)
        {
            if (col.gameObject.layer == 11)
            {
                Rigidbody rb = col.attachedRigidbody;
                if (rb != null)
                {
                    currentRigidbodies.Add(rb);
                    ProcessCube(rb);
                }
            }
        }

        // Soft Retention System
        if (retentionEnabled && intakeEntryPoint != null)
        {
            // Find all debris in the entire scene
            GameObject[] allDebris = GameObject.FindGameObjectsWithTag("Untagged"); // Primitive cubes might be untagged
            // Since we know the exact name:
            foreach (var debris in FindObjectsOfType<Rigidbody>())
            {
                if (debris.gameObject.layer == 11)
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

        // Cleanup dictionary for cubes that were consumed or left the area
        List<Rigidbody> keys = new List<Rigidbody>(cubeStages.Keys);
        foreach (Rigidbody rb in keys)
        {
            if (rb == null || !currentRigidbodies.Contains(rb))
            {
                cubeStages.Remove(rb);
                cubeStuckTimers.Remove(rb);
            }
        }
    }

    private void ProcessCube(Rigidbody rb)
    {
        if (!cubeStages.ContainsKey(rb))
        {
            // Determine starting stage based on X position relative to points
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

        // Determine target based on stage
        if (stage == 1)
        {
            targetPos = intakeEntryPoint.position;
            pullStrength = entryPullStrength;
            
            // If close enough to entry point, move to stage 2
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
            
            // If close enough to slope point, move to stage 3
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

        // Apply smooth velocity steering toward target
        Vector3 direction = targetPos - rb.position;
        float distance = direction.magnitude;

        if (distance > 0.05f)
        {
            Vector3 dirNorm = direction.normalized;

            Vector3 targetVelocity = dirNorm * maxIntakeSpeed;
            float lerpFactor = damping * pullStrength * Time.fixedDeltaTime;
            lerpFactor = Mathf.Clamp01(lerpFactor); // Prevent oscillation

            // Smoothly steer velocity
            rb.AddForce((targetVelocity - rb.linearVelocity) * (lerpFactor * 10f), ForceMode.Acceleration);
        }

        // IMPORTANT STUCK-CUBE SAFETY
        if (rb.linearVelocity.magnitude < 0.2f)
        {
            if (!cubeStuckTimers.ContainsKey(rb)) cubeStuckTimers[rb] = 0f;
            cubeStuckTimers[rb] += Time.fixedDeltaTime;

            if (cubeStuckTimers[rb] > 0.35f)
            {
                rb.WakeUp();
                // Apply a small additional force ALONG THE CHUTE direction
                Vector3 chuteDir = (targetPos - rb.position).normalized;
                // Add a little upward bump to overcome tiny colliders/lips
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
}