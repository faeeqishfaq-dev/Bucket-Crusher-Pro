with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('public void OnBoneCrushed(Transform hitTransform)\n    {\n        if (hitTransform == null)', 'public void OnBoneCrushed(Transform hitTransform)\n    {\n        UnityEngine.Debug.Log("[Voxel] OnBoneCrushed received: " + (hitTransform != null ? hitTransform.name : "null"));\n        if (hitTransform == null)')
content = content.replace('SpawnFallingBrick(visual);', 'UnityEngine.Debug.Log("[Voxel] Spawning falling brick");\n        SpawnFallingBrick(visual);')

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'w', encoding='utf-8') as f:
    f.write(content)
