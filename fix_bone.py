import re

with open('Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs', 'r', encoding='utf-8') as f:
    content = f.read()

pattern = r'GameObject bonePrefab = GameObject\.Find\(\"Bone\"\);'
replacement = """GameObject bonePrefab = null;
		foreach (GameObject go in Resources.FindObjectsOfTypeAll<GameObject>())
		{
			if (go.hideFlags == HideFlags.None && go.name == "Bone")
			{
				bonePrefab = go;
				break;
			}
		}"""
content = re.sub(pattern, replacement, content)

with open('Assets/Scripts/Assembly-CSharp/MapCreatorEditor.cs', 'w', encoding='utf-8') as f:
    f.write(content)
