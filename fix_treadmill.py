import re

with open('Assets/Scripts/Assembly-CSharp/TreadmillController.cs', 'r', encoding='utf-8') as f:
    content = f.read()

pattern = r'        GameObject ramp = new GameObject\("TreadmillEntranceRamp"\);\n        ramp\.transform\.SetParent\(transform, false\);\n        ramp\.layer = 11; // Debris\n        \n        // Right treadmill entrance is on the right \(local X = 0\.5\)\n        // Left treadmill entrance is on the left \(local X = -0\.5\)\n        ramp\.transform\.localPosition = new Vector3\(right \? 0\.55f : -0\.55f, -0\.2f, 0f\);\n        ramp\.transform\.localEulerAngles = new Vector3\(0f, 0f, right \? -30f : 30f\);\n        \n        BoxCollider rampCol = ramp\.AddComponent<BoxCollider>\(\);\n        rampCol\.size = new Vector3\(0\.3f, 1\.0f, 1\.5f\);'

replacement = """        GameObject ramp = new GameObject("TreadmillEntranceRamp");
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
        rampCol.size = new Vector3(0.3f, 1.0f, 1.5f);"""

content = re.sub(pattern, replacement, content)

with open('Assets/Scripts/Assembly-CSharp/TreadmillController.cs', 'w', encoding='utf-8') as f:
    f.write(content)
