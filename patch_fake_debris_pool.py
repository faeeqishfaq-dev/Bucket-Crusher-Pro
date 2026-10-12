import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

replacement = """public void ReturnBrickToPool(GameObject brick)
    {
        if (brick != null)
        {
            brick.SetActive(false);
            brickPool.Enqueue(brick);
        }
    }

    public void ReturnFakeBrickToPool(GameObject brick)
    {
        if (brick != null && brick.activeSelf && !fakeBrickPool.Contains(brick))
        {
            brick.SetActive(false);
            fakeBrickPool.Enqueue(brick);
        }
    }"""

text = re.sub(r'public void ReturnBrickToPool\(GameObject brick\)[\s\S]*?brickPool\.Enqueue\(brick\);\s*\}\s*\}', replacement, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Added ReturnFakeBrickToPool")
