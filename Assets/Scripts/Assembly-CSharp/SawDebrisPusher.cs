using UnityEngine;
using System.Collections.Generic;

public class SawDebrisPusher : MonoBehaviour
{
    [Header("Push Settings")]
    public bool pushEnabled = true;
    public float pushForce = 15f;
    public float maxPushSpeed = 8f;
    public float pushDamping = 5f;

    private Vector3 lastPos;
    private Vector3 realVelocity;

    private void Start()
    {
        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        realVelocity = (transform.position - lastPos) / Time.fixedDeltaTime;
        lastPos = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!pushEnabled) return;

        if (other.gameObject.layer == 11)
        {
            Rigidbody cubeRb = other.attachedRigidbody;
            if (cubeRb != null)
            {
                // Ensure dynamic
                cubeRb.isKinematic = false;
                
                // Wake up if sleeping
                if (cubeRb.IsSleeping()) cubeRb.WakeUp();

                // Get Saw movement velocity
                Vector3 sawVel = realVelocity;
                
                // Also check rigidbody velocity as fallback
                Rigidbody headRb = GetComponent<Rigidbody>();
                if (headRb != null && headRb.linearVelocity.magnitude > sawVel.magnitude)
                {
                    sawVel = headRb.linearVelocity;
                }
                
                // Only push if the saw is actually moving
                if (sawVel.magnitude > 0.5f)
                {
                    Vector3 pushDir = sawVel.normalized;
                    
                    // Add a tiny bit of upward force so they don't grind into the floor
                    pushDir.y += 0.2f;
                    pushDir.Normalize();

                    Vector3 targetVel = pushDir * Mathf.Min(sawVel.magnitude * 1.5f, maxPushSpeed);
                    Vector3 velDiff = targetVel - cubeRb.linearVelocity;
                    
                    cubeRb.AddForce(velDiff * pushForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
                }
            }
        }
    }
}
