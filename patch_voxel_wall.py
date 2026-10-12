import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

optimized = """        for (int i = 0; i < activeBones.Count; i++)
        {
            Transform bone = activeBones[i];
            if (bone == null) continue;
            
            // Avoid calling GetBlockVisual on crushed bones
            if (!visualCache.ContainsKey(bone)) continue;

            if (bone.localScale.sqrMagnitude < 0.01f) continue;

            BlockVisual visual = visualCache[bone];

            Matrix4x4 matrix = Matrix4x4.TRS(visual.position, visual.rotation, visual.scale);
            groups[visual.materialIndex].Add(matrix);"""

text = re.sub(r'for \(int i = 0; i < activeBones\.Count; i\+\)[\s\S]*?groups\[visual\.materialIndex\]\.Add\(matrix\);', optimized, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Optimized VoxelWallConverter.cs")
