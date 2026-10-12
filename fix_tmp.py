import re

fixes = [
    (
        'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/MonoBehaviour/LeagueSpartan-Bold SDF.asset',
        'm_AtlasTextures:\n  - {fileID: 2800000, guid: 3407fee928581cd4681aa48e83b134ac, type: 3}'
    ),
    (
        'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/MonoBehaviour/LiberationSans SDF - VS - Fallback.asset',
        'm_AtlasTextures:\n  - {fileID: 2800000, guid: b72f5f6302927d84db61945c8b394b15, type: 3}'
    ),
    (
        'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/MonoBehaviour/LiberationSans SDF - VS.asset',
        'm_AtlasTextures:\n  - {fileID: 2800000, guid: b72f5f6302927d84db61945c8b394b15, type: 3}'
    )
]

for path, new_text in fixes:
    with open(path, 'r', encoding='utf-8') as f:
        text = f.read()
    
    text = re.sub(r'm_AtlasTextures:\n  - \{fileID: 0\}', new_text, text)
    
    with open(path, 'w', encoding='utf-8') as f:
        f.write(text)
    
    print(f'Fixed {path}')
