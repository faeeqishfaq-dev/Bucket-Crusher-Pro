import re
import os

scene_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scenes/GamePlay.unity'

with open(scene_path, 'r', encoding='utf-8') as f:
    text = f.read()

# 1. 36873 (ExcavatorBoosterController m_Target)
text = text.replace('m_Target: {fileID: 36873}', 'm_Target: {fileID: 0}')

# 2. 36921 (m_TargetGraphic) -> Image 37111
text = text.replace('m_TargetGraphic: {fileID: 36921}', 'm_TargetGraphic: {fileID: 37111}')

# 3. 37026 (m_TargetGraphic) -> None (0)
text = text.replace('m_TargetGraphic: {fileID: 37026}', 'm_TargetGraphic: {fileID: 0}')

# 4. 37379 (m_TargetGraphic) -> None (0)
text = text.replace('m_TargetGraphic: {fileID: 37379}', 'm_TargetGraphic: {fileID: 0}')

# 5. 36884 (m_TargetGraphic) -> Text 36365
text = text.replace('m_TargetGraphic: {fileID: 36884}', 'm_TargetGraphic: {fileID: 36365}')

# 6. 37444 (m_TargetGraphic) -> Image 36623
text = text.replace('m_TargetGraphic: {fileID: 37444}', 'm_TargetGraphic: {fileID: 36623}')

# 7. 37098 (EcoBar) -> None (0)
text = text.replace('EcoBar: {fileID: 37098}', 'EcoBar: {fileID: 0}')

with open(scene_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Fixed broken PPtrs")
