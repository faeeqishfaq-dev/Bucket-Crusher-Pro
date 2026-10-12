import re

voxel_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
with open(voxel_file, 'r', encoding='utf-8') as f:
    text = f.read()

rebuild_repl = """        for (int i = 0; i < activeBones.Count; i++)
        {
            Transform bone = activeBones[i];
            if (bone == null) continue;
            
            // Skip destroyed bones (O(1) lookup instead of O(N) Remove)
            if (!visualCache.ContainsKey(bone)) continue;

            if (bone.localScale.sqrMagnitude < 0.01f) continue;

            BlockVisual visual = visualCache[bone];

            Matrix4x4 matrix = Matrix4x4.TRS(visual.position, visual.rotation, visual.scale);"""
text = re.sub(r'for \(int i = 0; i < activeBones\.Count; i\+\)[\s\S]*?Matrix4x4 matrix = Matrix4x4\.TRS\(visual\.position, visual\.rotation, visual\.scale\);', rebuild_repl, text)

with open(voxel_file, 'w', encoding='utf-8') as f:
    f.write(text)

print('Patched VoxelWallConverter.cs lag')
