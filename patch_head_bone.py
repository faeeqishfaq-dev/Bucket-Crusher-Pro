import re

head_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Head.cs'
with open(head_file, 'r', encoding='utf-8') as f:
    text = f.read()

head_repl = """			AddPhysics(other);

			if (VoxelWallConverter.Instance != null)
			{
				VoxelWallConverter.Instance.OnBoneCrushed(other.transform);
			}

			if (progress != null)"""
text = text.replace("""			AddPhysics(other);

			if (progress != null)""", head_repl)

with open(head_file, 'w', encoding='utf-8') as f:
    f.write(text)

print('Patched Head.cs')
