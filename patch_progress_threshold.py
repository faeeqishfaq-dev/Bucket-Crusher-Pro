import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Progress.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

# Replace 0.90f with 0.995f in GetProgressPct
text = text.replace('if (progress >= 0.90f)', 'if (progress >= 0.995f)')

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Updated Progress.cs thresholds to 0.995f")
