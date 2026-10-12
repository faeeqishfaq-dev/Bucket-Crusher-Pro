with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('UnityEngine.Debug.Log("[Voxel] OnBoneCrushed received: " + (hitTransform != null ? hitTransform.name : "null"));\n        if (hitTransform == null)', 'if (hitTransform == null)')
content = content.replace('UnityEngine.Debug.Log("[Voxel] Spawning falling brick");\n        SpawnFallingBrick(visual);', 'SpawnFallingBrick(visual);')

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'w', encoding='utf-8') as f:
    f.write(content)
