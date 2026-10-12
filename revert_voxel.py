import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

original = """        for (int i = 0; i < activeBones.Count; i++)
        {
            Transform bone = activeBones[i];
            if (bone == null) continue;
            if (bone.localScale.sqrMagnitude < 0.01f) continue;

            BlockVisual visual;
            if (!visualCache.TryGetValue(bone, out visual))
                visual = GetBlockVisual(bone);

            Matrix4x4 matrix = Matrix4x4.TRS(visual.position, visual.rotation, visual.scale);
            groups[visual.materialIndex].Add(matrix);
            groupsColors[visual.materialIndex].Add(visual.color);"""

text = re.sub(r'for \(int i = 0; i < activeBones\.Count; i\+\)[\s\S]*?groups\[visual\.materialIndex\]\.Add\(matrix\);', original, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print('Reverted VoxelWallConverter.cs')
