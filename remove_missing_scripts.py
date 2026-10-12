import os, glob, re

target_guid = '81b090e094d22b969d28b70d69c1ee0b'
files_to_check = glob.glob('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/**/*.prefab', recursive=True) + \
                 glob.glob('d:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/**/*.unity', recursive=True)

files_changed = 0
components_removed = 0

for f_path in files_to_check:
    with open(f_path, 'r', encoding='utf-8') as f:
        content = f.read()

    if target_guid not in content:
        continue

    # Find fileIDs of MonoBehaviours with the missing GUID
    pattern = r'--- !u!114 &(\d+)\s+MonoBehaviour:[\s\S]*?m_Script: \{fileID: 11500000, guid: ' + target_guid
    
    file_ids_to_remove = set()
    for m in re.finditer(pattern, content):
        file_ids_to_remove.add(m.group(1))
    
    if not file_ids_to_remove:
        continue
        
    lines = content.split('\n')
    new_lines = []
    
    for line in lines:
        match = re.search(r'-\s*component:\s*\{fileID:\s*(\d+)\}', line)
        if match and match.group(1) in file_ids_to_remove:
            components_removed += 1
            continue
            
        new_lines.append(line)
        
    content = '\n'.join(new_lines)
    
    for file_id in file_ids_to_remove:
        # Match the block up to the next '--- !u!' or end of string
        block_pattern = r'--- !u!114 &' + file_id + r'\b[\s\S]*?(?=\n--- |\Z)'
        content = re.sub(block_pattern, '', content)
        
    with open(f_path, 'w', encoding='utf-8') as f:
        f.write(content)
        
    files_changed += 1

print(f'Removed {components_removed} missing scripts across {files_changed} files.')
