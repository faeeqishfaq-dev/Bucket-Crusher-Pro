import re

voxel = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
with open(voxel, 'r', encoding='utf-8') as f:
    text = f.read()

missing_methods = """
    public void ResetBlocks()
    {
        Map map = GetComponent<Map>();
        if (map == null) map = UnityEngine.Object.FindAnyObjectByType<Map>();
        if (map == null || map.Blocks == null) return;

        FindSourceRenderer();
        if (cubeMesh == null) CreateCubeMesh();
        CreateBrickMaterials();

        MapCreatorEditor mce = transform.parent != null ? transform.parent.GetComponent<MapCreatorEditor>() : UnityEngine.Object.FindAnyObjectByType<MapCreatorEditor>();
        if (mce != null && mce.CurrentLevel != null)
        {
            currentTexture = mce.CurrentLevel.Sprite;
        }

        activeBones.Clear();
        activeBones.AddRange(map.Blocks);
        ActiveBonesCount = activeBones.Count;
        
        visualCache.Clear();
        foreach (Transform bone in activeBones)
        {
            if (bone != null)
            {
                bone.gameObject.layer = 10;
                visualCache[bone] = GetBlockVisual(bone);
            }
        }
        isDirty = true;
    }

    public void OnBoneCrushed(Transform hitTransform)
    {
        Transform bone = FindActualWallBone(hitTransform);
        if (bone == null) return;

        BlockVisual visual;
        if (!visualCache.TryGetValue(bone, out visual))
            visual = GetBlockVisual(bone);

        // activeBones.Remove(bone); // O(N) removed
        visualCache.Remove(bone);
        ActiveBonesCount--;

        isDirty = true;

        SpawnFallingBrick(visual);
    }

    private Transform FindActualWallBone(Transform hit)
    {
        if (visualCache.ContainsKey(hit)) return hit;
        
        Transform current = hit.parent;
        while (current != null)
        {
            if (visualCache.ContainsKey(current)) return current;
            current = current.parent;
        }
        return null;
    }

    // --------------------------------------------------"""

# Insert right before // FALLING REAL PHYSICS BLOCK
text = text.replace('    // --------------------------------------------------\n    // FALLING REAL PHYSICS BLOCK', missing_methods + '\n    // FALLING REAL PHYSICS BLOCK')

with open(voxel, 'w', encoding='utf-8') as f:
    f.write(text)

print('Restored missing methods.')
