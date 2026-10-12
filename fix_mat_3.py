import os
with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'r', encoding='utf-8') as f:
    content = f.read()

start_idx = content.find('private void CreateBrickMaterials()')
end_idx = content.find('private void CreateIntakeTrigger()')

if start_idx != -1 and end_idx != -1:
    new_method = '''private void CreateBrickMaterials()
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

    '''
    content = content[:start_idx] + new_method + content[end_idx:]

# also remove materialIndex logic
content = content.replace('int materialIndex =\n            rng.Next(0, MATERIAL_VARIANTS);', '')
content = content.replace('int materialIndex = rng.Next(0, MATERIAL_VARIANTS);', '')
content = content.replace('result.materialIndex =\n            materialIndex;', 'result.materialIndex = 0;')
content = content.replace('result.materialIndex = materialIndex;', 'result.materialIndex = 0;')

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'w', encoding='utf-8') as f:
    f.write(content)
