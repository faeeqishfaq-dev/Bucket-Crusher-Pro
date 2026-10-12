import re
import os

voxel_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
with open(voxel_path, 'r', encoding='utf-8') as f:
    text = f.read()

# 1. Change ActiveBonesCount to an auto-property
text = text.replace('public int ActiveBonesCount => activeBones.Count;', 'public int ActiveBonesCount { get; private set; }')

# 2. Update ActiveBonesCount in Start()
start_repl = """        if (map.Blocks != null)
        {
            activeBones.AddRange(map.Blocks);
            ActiveBonesCount = activeBones.Count;"""
text = text.replace("""        if (map.Blocks != null)
        {
            activeBones.AddRange(map.Blocks);""", start_repl)

# 3. Update ActiveBonesCount in ResetBlocks()
reset_repl = """        activeBones.Clear();
        activeBones.AddRange(map.Blocks);
        ActiveBonesCount = activeBones.Count;"""
text = text.replace("""        activeBones.Clear();
        activeBones.AddRange(map.Blocks);""", reset_repl)

# 4. Decrease ActiveBonesCount in OnBoneCrushed()
crush_repl = """        // activeBones.Remove(bone); // O(N) removed
        visualCache.Remove(bone);
        ActiveBonesCount--;"""
text = text.replace("""        // activeBones.Remove(bone); // O(N) removed
        visualCache.Remove(bone);""", crush_repl)

with open(voxel_path, 'w', encoding='utf-8') as f:
    f.write(text)

print('Patched VoxelWallConverter.cs to fix ActiveBonesCount')
