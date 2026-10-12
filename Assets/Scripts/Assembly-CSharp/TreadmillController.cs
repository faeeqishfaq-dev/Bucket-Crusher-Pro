using FlatKit;
using UnityEngine;
using System.Collections.Generic;

public class TreadmillController : MonoBehaviour
{
    public bool IsPlayer;

    private float multiplier;

    public GameObject Visuals;

    public Item TreadmillItem;

    public UvScrollerMat scroll;

    [SerializeField]
    private bool right;

    [Header("Conveyor Physics")]
    public float conveyorSpeed = 5f;
    public float conveyorForce = 15f;
    public float maxCubeSpeed = 6f;
    // Moving towards the left (into the machine)
    public Vector3 conveyorDirection = new Vector3(-1f, 0f, 0f);

    private Dictionary<Rigidbody, float> stuckTimers = new Dictionary<Rigidbody, float>();

    private void Start()
    {
        // Add low friction material to child colliders
        PhysicsMaterial frictionless = new PhysicsMaterial("TreadmillFrictionless");
        frictionless.staticFriction = 0f;
        frictionless.dynamicFriction = 0f;
        frictionless.bounciness = 0f;
        frictionless.frictionCombine = PhysicsMaterialCombine.Minimum;
        frictionless.bounceCombine = PhysicsMaterialCombine.Minimum;

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            if (!col.isTrigger)
            {
                col.material = frictionless;
            }
        }

        // Fix the lip gap!
        // The treadmill is slightly higher than the ground, causing cubes to hit a wall.
        // Create a small frictionless ramp at the outer edge to guide them up.
        GameObject ramp = new GameObject("TreadmillEntranceRamp");
        ramp.transform.SetParent(transform.parent, false); // Parent to excavator to avoid negative scale inheritance
        ramp.layer = 11; // Debris
        
        if (right)
        {
            ramp.transform.localPosition = transform.localPosition + new Vector3(1.4f, -0.05f, 0f);
            ramp.transform.localEulerAngles = new Vector3(0f, 0f, -30f);
        }
        else
        {
            ramp.transform.localPosition = transform.localPosition + new Vector3(-1.4f, -0.05f, 0f);
            ramp.transform.localEulerAngles = new Vector3(0f, 0f, 30f);
        }
        
        BoxCollider rampCol = ramp.AddComponent<BoxCollider>();
        rampCol.size = new Vector3(0.3f, 1.0f, 1.5f);
        rampCol.material = frictionless;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                // Ensure dynamic and active
                rb.isKinematic = false;
                
                // Determine direction based on 'right' flag
                // The machine faces right (positive X). The intake is at X=0 relative to the treadmills.
                // Right treadmill is at X > 0, so it should push left (negative X).
                // Left treadmill is at X < 0, so it should push right (positive X).
                Vector3 dir = right ? new Vector3(-1f, 0f, 0f) : new Vector3(1f, 0f, 0f);
                
                // Use overridden direction if not default
                if (conveyorDirection != new Vector3(-1f, 0f, 0f) && right) dir = conveyorDirection;

                Vector3 targetVel = dir.normalized * maxCubeSpeed;
                Vector3 velDiff = targetVel - rb.linearVelocity;
                
                // Steering
                rb.AddForce(velDiff * conveyorForce * Time.fixedDeltaTime, ForceMode.VelocityChange);
                
                // Anti-stuck behavior
                if (rb.linearVelocity.magnitude < 0.2f)
                {
                    if (!stuckTimers.ContainsKey(rb)) stuckTimers[rb] = 0f;
                    stuckTimers[rb] += Time.fixedDeltaTime;
                    if (stuckTimers[rb] > 0.35f)
                    {
                        rb.WakeUp();
                        rb.AddForce((dir.normalized + Vector3.up * 0.5f).normalized * 0.5f, ForceMode.Impulse);
                        stuckTimers[rb] = 0f;
                    }
                }
                else
                {
                    stuckTimers[rb] = 0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                stuckTimers.Remove(rb);
            }
        }
    }

    private void Update()
    {
    }
}
