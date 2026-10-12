import re

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('CreateIntakeTrigger();\n\n        RebuildMatrices();', 'CreateIntakeTrigger();\n        CreateIntakeChute();\n\n        RebuildMatrices();')

chute_code = """
    private void CreateIntakeChute()
    {
        GameObject excavator = GameObject.Find("excavator");
        if (excavator == null) return;

        // Fix Cube (3) blocking the intake
        Transform hopper = excavator.transform.Find("Top/Cube (3)");
        if (hopper != null)
        {
            Collider hopperCol = hopper.GetComponent<Collider>();
            if (hopperCol != null) hopperCol.isTrigger = true; // DO NOT block debris physically!
        }

        // Create zero friction material
        PhysicMaterial frictionless = new PhysicMaterial("FrictionlessChute");
        frictionless.staticFriction = 0f;
        frictionless.dynamicFriction = 0f;
        frictionless.bounciness = 0f;
        frictionless.frictionCombine = PhysicMaterialCombine.Minimum;
        frictionless.bounceCombine = PhysicMaterialCombine.Minimum;

        // Create the ramp
        GameObject ramp = new GameObject("IntakeChuteRamp");
        ramp.transform.SetParent(excavator.transform, false);
        ramp.layer = 11; // Debris layer, so it interacts with debris
        
        // Slope from X=0.5, Y=0.5 up to X=-1.3, Y=1.5
        // Distance X is 1.8, Y is 1.0. Angle is atan(1/1.8) = 29 degrees.
        ramp.transform.localPosition = new Vector3(-0.4f, 1.0f, 0f);
        ramp.transform.localEulerAngles = new Vector3(0f, 0f, -29f); // Negative angle raises the left side (hopper side is at -1.3)
        
        BoxCollider rampCol = ramp.AddComponent<BoxCollider>();
        rampCol.size = new Vector3(2.5f, 0.1f, 2.0f);
        rampCol.material = frictionless;
    }
"""

idx = content.find('magnet.intakeConsumePoint = p3.transform;')
if idx != -1:
    end_idx = content.find('}', idx) + 1
    content = content[:end_idx] + chute_code + content[end_idx:]

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'w', encoding='utf-8') as f:
    f.write(content)
