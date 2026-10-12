import re

with open('Assets/Scripts/Assembly-CSharp/MachineIntakeMagnet.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Add new fields
pattern_fields = r'    public float consumeDistance = 0.2f;'
replacement_fields = """    public float consumeDistance = 0.2f;

    [Header("Soft Retention System")]
    public bool retentionEnabled = true;
    public float maxDebrisDistance = 6.0f;
    public float returnForce = 5f;
    public float maxReturnSpeed = 3f;"""
content = re.sub(pattern_fields, replacement_fields, content)

# Add retention logic to FixedUpdate
pattern_update = r'        // Cleanup dictionary for cubes that were consumed or left the area'
replacement_update = """        // Soft Retention System
        if (retentionEnabled && intakeEntryPoint != null)
        {
            // Find all debris in the entire scene
            GameObject[] allDebris = GameObject.FindGameObjectsWithTag("Untagged"); // Primitive cubes might be untagged
            // Since we know the exact name:
            foreach (var debris in FindObjectsOfType<Rigidbody>())
            {
                if (debris.gameObject.name == "BrokenWallBrick")
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
                        debris.linearVelocity = Vector3.Lerp(debris.linearVelocity, targetVelocity, lerpFactor);
                    }
                }
            }
        }

        // Cleanup dictionary for cubes that were consumed or left the area"""
content = re.sub(pattern_update, replacement_update, content)

with open('Assets/Scripts/Assembly-CSharp/MachineIntakeMagnet.cs', 'w', encoding='utf-8') as f:
    f.write(content)
