import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/SawDebrisPusher.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """public class SawDebrisPusher : MonoBehaviour
{
    [Header("Push Settings")]
    public bool pushEnabled = true;
    public float pushForce = 15f;
    public float maxPushSpeed = 8f;
    public float pushDamping = 5f;

    private Vector3 lastPos;
    private Vector3 realVelocity;
    private Rigidbody headRb;

    private void Start()
    {
        lastPos = transform.position;
        headRb = GetComponent<Rigidbody>();
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
                
                // Use cached headRb
                if (headRb != null && headRb.linearVelocity.sqrMagnitude > sawVel.sqrMagnitude)
                {
                    sawVel = headRb.linearVelocity;
                }
                
                // Only push if the saw is actually moving
                if (sawVel.sqrMagnitude > 0.25f)
                {
                    Vector3 pushDir = sawVel.normalized;
                    
                    // Add a tiny bit of upward force so they don't grind into the floor
                    pushDir.y += 0.2f;
                    pushDir.Normalize();

                    Vector3 targetVel = pushDir * Mathf.Min(sawVel.magnitude * 1.5f, maxPushSpeed);
                    Vector3 velDiff = targetVel - cubeRb.linearVelocity;
                    
                    cubeRb.AddForce(velDiff * (pushForce * Time.fixedDeltaTime), ForceMode.VelocityChange);
                }
            }
        }
    }
}"""

text = re.sub(r'public class SawDebrisPusher : MonoBehaviour[\s\S]*\}\s*\}', optimized, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Optimized SawDebrisPusher.cs")
