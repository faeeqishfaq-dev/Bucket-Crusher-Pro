import re

with open('Assets/Scripts/Assembly-CSharp/MachineIntakeMagnet.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Add Dictionary
pattern_dict = r'private Dictionary<Rigidbody, int> cubeStages = new Dictionary<Rigidbody, int>\(\);'
replacement_dict = """private Dictionary<Rigidbody, int> cubeStages = new Dictionary<Rigidbody, int>();
    private Dictionary<Rigidbody, float> cubeStuckTimers = new Dictionary<Rigidbody, float>();"""
content = re.sub(pattern_dict, replacement_dict, content)

# Clean up Dictionary
pattern_clean = r'cubeStages\.Remove\(rb\);\n            \}'
replacement_clean = """cubeStages.Remove(rb);
                cubeStuckTimers.Remove(rb);
            }"""
content = re.sub(pattern_clean, replacement_clean, content)

# Add Anti-Stuck Logic
pattern_process = r'rb\.linearVelocity = Vector3\.Lerp\(rb\.linearVelocity, targetVelocity, lerpFactor\);\n        \}'
replacement_process = """rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, lerpFactor);
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
        }"""
content = re.sub(pattern_process, replacement_process, content)

with open('Assets/Scripts/Assembly-CSharp/MachineIntakeMagnet.cs', 'w', encoding='utf-8') as f:
    f.write(content)
