import re
with open('Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs', 'r', encoding='utf-8') as f:
    content = f.read()

pattern = r'float spacingX = 0\.18f / 100f;\s*float spacingY = 0\.11f / 100f;\s*int width = _currentSprite\.width;\s*int height = _currentSprite\.height;\s*float offsetX = \(width \* spacingX\) / 2f;'
replacement = '''int step = 7;
		float spacingX = 0.077f / 100f;
		float spacingY = 0.055f / 100f;
		int width = _currentSprite.width;
		int height = _currentSprite.height;
		float offsetX = ((width / step) * spacingX) / 2f;'''

content = re.sub(pattern, replacement, content)

pattern_loop = r'for \(int x = 0; x < width; x\+\+\)\s*\{\s*for \(int y = 0; y < height; y\+\+\)'
replacement_loop = '''for (int x = 0; x < width; x += step)
		{
			for (int y = 0; y < height; y += step)'''

content = re.sub(pattern_loop, replacement_loop, content)

pattern_pos = r'newBone\.transform\.localPosition = startPos \+ new Vector3\(x \* spacingX - offsetX, y \* spacingY, 0f\);'
replacement_pos = '''newBone.transform.localPosition = startPos + new Vector3((x / step) * spacingX - offsetX, (y / step) * spacingY, 0f);'''

content = re.sub(pattern_pos, replacement_pos, content)

with open('Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs', 'w', encoding='utf-8') as f:
    f.write(content)
