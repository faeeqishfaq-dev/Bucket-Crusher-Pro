import re
with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'r', encoding='utf-8') as f:
    content = f.read()

pattern = r'private void CreateBrickMaterials\(\).*?private void CreateIntakeTrigger\(\)'
replacement = '''private void CreateBrickMaterials()
    {
        if (smr != null && smr.sharedMaterial != null)
        {
            Material originalMat = smr.sharedMaterial;
            brickMaterials = new Material[1];
            brickMaterials[0] = new Material(originalMat);
            brickMaterials[0].enableInstancing = true;
        }
        else
        {
            brickMaterials = new Material[1];
            Shader shader = Shader.Find("Standard");
            brickMaterials[0] = new Material(shader);
            brickMaterials[0].enableInstancing = true;
        }
    }

    private void CreateIntakeTrigger()'''

content = re.sub(pattern, replacement, content, flags=re.DOTALL)
# also remove materialIndex logic
content = re.sub(r'int materialIndex =[^;]+;\s*BlockVisual result', 'BlockVisual result', content)
content = content.replace('result.materialIndex =\n            materialIndex;', 'result.materialIndex = 0;')

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'w', encoding='utf-8') as f:
    f.write(content)
