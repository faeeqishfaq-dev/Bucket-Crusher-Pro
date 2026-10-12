import re

with open('Assets/Scripts/Assembly-CSharp/Head.cs', 'r', encoding='utf-8') as f:
    content = f.read()

content = content.replace('other.gameObject.name.StartsWith("Bone")', 'other.gameObject.name.StartsWith("Bone.")')

with open('Assets/Scripts/Assembly-CSharp/Head.cs', 'w', encoding='utf-8') as f:
    f.write(content)
