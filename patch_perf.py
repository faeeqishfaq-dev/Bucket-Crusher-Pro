import re
import os

def patch_file(path, replacements):
    if not os.path.exists(path):
        print(f"File not found: {path}")
        return
    with open(path, 'r', encoding='utf-8') as f:
        text = f.read()
    
    for old, new in replacements:
        if old in text:
            text = text.replace(old, new)
        else:
            print(f"Warning: String not found in {path}:\n{old}")
            
    with open(path, 'w', encoding='utf-8') as f:
        f.write(text)

# 1. Update DebrisIntake sizes
debris_intake_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/DebrisIntake.cs'
debris_replacements = [
    ('leftWall.transform.localPosition = new Vector3(-2.882f, -1.0f, 0f);', 'leftWall.transform.localPosition = new Vector3(-3.407f, -1.0f, 0f);'),
    ('leftBox.size = new Vector3(1.0f, 4.0f, 2.0f);', 'leftBox.size = new Vector3(1.0f, 5.0f, 2.7f);'),
    ('rightWall.transform.localPosition = new Vector3(0.118f, -1.0f, 0f);', 'rightWall.transform.localPosition = new Vector3(0.643f, -1.0f, 0f);'),
    ('rightBox.size = new Vector3(1.0f, 4.0f, 2.0f);', 'rightBox.size = new Vector3(1.0f, 5.0f, 2.7f);'),
    ('solidBox.size = new Vector3(3.0f, 1.0f, 2.0f);', 'solidBox.size = new Vector3(4.05f, 1.0f, 2.7f);'),
    ('triggerBox.size = new Vector3(3.0f, 3.0f, 2.0f);', 'triggerBox.size = new Vector3(4.05f, 4.0f, 2.7f);')
]
patch_file(debris_intake_path, debris_replacements)

# 2. Disable physical collision for layer 11 in VoxelWallConverter
voxel_wall_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
voxel_replacements = [
    ('private void Start()\n    {', 'private void Start()\n    {\n        UnityEngine.Physics.IgnoreLayerCollision(11, 11, true);')
]
patch_file(voxel_wall_path, voxel_replacements)

# 3. Remove WakeUp in SawDebrisPusher
saw_pusher_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/SawDebrisPusher.cs'
saw_replacements = [
    ('if (cubeRb.IsSleeping()) cubeRb.WakeUp();', '// if (cubeRb.IsSleeping()) cubeRb.WakeUp(); // Optimize: Allow sleep')
]
patch_file(saw_pusher_path, saw_replacements)

# 4. Remove WakeUp in MachineIntakeMagnet
machine_magnet_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/MachineIntakeMagnet.cs'
machine_replacements = [
    ('if (debris.IsSleeping()) debris.WakeUp();', '// if (debris.IsSleeping()) debris.WakeUp(); // Optimize: Allow sleep')
]
patch_file(machine_magnet_path, machine_replacements)

# 5. Remove WakeUp in TreadmillController
treadmill_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/TreadmillController.cs'
treadmill_replacements = [
    ('rb.WakeUp();', '// rb.WakeUp(); // Optimize: Allow sleep')
]
patch_file(treadmill_path, treadmill_replacements)

print("Optimization patch applied")
