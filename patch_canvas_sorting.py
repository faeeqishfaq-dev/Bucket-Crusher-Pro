import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scenes/GamePlay.unity'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

def replacer(match):
    block = match.group(0)
    block = block.replace('m_OverrideSorting: 0', 'm_OverrideSorting: 1')
    block = re.sub(r'm_SortingOrder: \d+', 'm_SortingOrder: 100', block)
    return block

# Find the canvas block for 34981
new_text = re.sub(r'--- !u!223 &34981\n.*?(?=\n---|\Z)', replacer, text, flags=re.DOTALL)
# wait, the class ID for Canvas is 223 usually. In the file it was '--- !u!\d+ &34981'
new_text = re.sub(r'--- !u!\d+ &34981\n.*?(?=\n---|\Z)', replacer, text, flags=re.DOTALL)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(new_text)

print("Updated ExcavatorMultiplier Canvas sorting order to 100")
