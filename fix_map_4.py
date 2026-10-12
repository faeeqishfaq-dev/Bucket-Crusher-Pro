import os
with open('Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Remove the offsetX calculation entirely
content = content.replace('float offsetX = ((width / step) * spacingX) / 2f;', 'float offsetX = 0f;')

# Change the localPosition calculation to use negative X spacing and add 0.005848 offset if needed? No, just start at 0
content = content.replace('newBone.transform.localPosition = startPos + new Vector3((x / step) * spacingX - offsetX, (y / step) * spacingY, 0f);', 'newBone.transform.localPosition = startPos + new Vector3(-((float)x / step) * spacingX, ((float)y / step) * spacingY, 0f);')

with open('Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs', 'w', encoding='utf-8') as f:
    f.write(content)
