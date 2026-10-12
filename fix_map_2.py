import os
with open('Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs', 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('float spacingX = 0.18f / 100f;', 'int step = 7;\n\t\tfloat spacingX = 0.077f / 100f;')
content = content.replace('float spacingY = 0.11f / 100f;', 'float spacingY = 0.055f / 100f;')
content = content.replace('float offsetX = (width * spacingX) / 2f;', 'float offsetX = ((width / step) * spacingX) / 2f;')

content = content.replace('for (int y = 0; y < height; y++)', 'for (int y = 0; y < height; y += step)')
content = content.replace('for (int x = 0; x < width; x++)', 'for (int x = 0; x < width; x += step)')

content = content.replace('newBone.transform.localPosition = startPos + new Vector3(x * spacingX - offsetX, y * spacingY, 0f);', 'newBone.transform.localPosition = startPos + new Vector3((x / step) * spacingX - offsetX, (y / step) * spacingY, 0f);')

with open('Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs', 'w', encoding='utf-8') as f:
    f.write(content)
