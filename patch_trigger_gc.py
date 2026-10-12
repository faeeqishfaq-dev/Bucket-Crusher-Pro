import glob, re
import os

count = 0
for f in glob.glob('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/*.cs'):
    with open(f, 'r', encoding='utf-8') as file:
        text = file.read()
    
    if 'other.gameObject.name == "BrokenWallBrick"' in text or 'other.name == "BrokenWallBrick"' in text:
        text = text.replace('other.gameObject.name == "BrokenWallBrick"', 'other.gameObject.layer == 11')
        text = text.replace('other.name == "BrokenWallBrick"', 'other.gameObject.layer == 11')
        
        with open(f, 'w', encoding='utf-8') as file:
            file.write(text)
        count += 1
        print('Patched GC allocation in', os.path.basename(f))

print('Total files patched for GC string allocation:', count)
