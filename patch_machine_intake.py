f = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/MachineIntakeMagnet.cs'
with open(f, 'r', encoding='utf-8') as file:
    text = file.read()
text = text.replace('col.gameObject.name == "BrokenWallBrick"', 'col.gameObject.layer == 11')
text = text.replace('debris.gameObject.name == "BrokenWallBrick"', 'debris.gameObject.layer == 11')
with open(f, 'w', encoding='utf-8') as file:
    file.write(text)
print('Patched MachineIntakeMagnet.cs')
