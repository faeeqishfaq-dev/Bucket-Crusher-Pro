import re
with open('Assets/Scenes/GamePlay.unity', 'r', encoding='utf-8') as f:
    content = f.read()

def get_name_and_pos(transform_id):
    for b in content.split('--- !u!'):
        if f'&{transform_id}\n' in b:
            pos = re.search(r'm_LocalPosition:\s*(\{.*\})', b)
            anch = re.search(r'm_AnchoredPosition:\s*(\{.*\})', b)
            go_id = re.search(r'm_GameObject:\s*\{fileID:\s*(\d+)\}', b)
            name = 'unknown'
            if go_id:
                for b2 in content.split('--- !u!'):
                    if 'GameObject:' in b2 and f'&{go_id.group(1)}\n' in b2:
                        name_match = re.search(r'm_Name:\s*(.*)', b2)
                        if name_match: name = name_match.group(1)
            parent = re.search(r'm_Father:\s*\{fileID:\s*(\d+)\}', b)
            pos_str = pos.group(1) if pos else "None"
            parent_str = parent.group(1) if parent else "None"
            print("ID:", transform_id, "Name:", name, "Pos:", pos_str, "Parent:", parent_str)
            if parent and parent.group(1) != '0':
                get_name_and_pos(parent.group(1))
            break

get_name_and_pos('35539')
