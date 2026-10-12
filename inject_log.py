import re

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'r', encoding='utf-8') as f:
    content = f.read()

inject = """
    private bool loggedState = false;
    private void Update()
    {
        if (!loggedState)
        {
            loggedState = true;
            Debug.Log("[WALL_DEBUG] Map!=null: " + (GetComponent<Map>()!=null));
            Debug.Log("[WALL_DEBUG] Map.Blocks: " + (GetComponent<Map>()?.Blocks?.Count ?? 0));
            Debug.Log("[WALL_DEBUG] activeBones: " + activeBones.Count);
            Debug.Log("[WALL_DEBUG] visualCache: " + visualCache.Count);
            Debug.Log("[WALL_DEBUG] drawBatches: " + drawBatches.Count);
            Debug.Log("[WALL_DEBUG] cubeMesh: " + (cubeMesh != null));
            Debug.Log("[WALL_DEBUG] brickMaterials: " + (brickMaterials != null));
            
            if (activeBones.Count > 0 && drawBatches.Count == 0) {
                Debug.Log("[WALL_DEBUG] checking bone 0 scale: " + (activeBones[0] != null ? activeBones[0].localScale.sqrMagnitude.ToString() : "null"));
            }
        }
"""

content = content.replace('private void Update()\n    {', inject)

with open('Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs', 'w', encoding='utf-8') as f:
    f.write(content)
