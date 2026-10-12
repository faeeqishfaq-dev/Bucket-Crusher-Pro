import re

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'r', encoding='utf-8') as f:
    content = f.read()

pattern = r'BoxCollider box = brickObj\.AddComponent<BoxCollider>\(\);\n\s*box\.size = new Vector3\((.*?)\);'
replacement = """BoxCollider box = brickObj.AddComponent<BoxCollider>();
        box.size = new Vector3(\\1);
        
        PhysicMaterial lowFriction = new PhysicMaterial("LowFrictionDebris");
        lowFriction.dynamicFriction = 0.1f;
        lowFriction.staticFriction = 0.1f;
        lowFriction.bounciness = 0.1f;
        lowFriction.frictionCombine = PhysicMaterialCombine.Minimum;
        box.material = lowFriction;"""

content = re.sub(pattern, replacement, content)

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'w', encoding='utf-8') as f:
    f.write(content)
