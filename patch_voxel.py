import re
import os

voxel_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
with open(voxel_path, 'r', encoding='utf-8') as f:
    text = f.read()

# Replace FindActualWallBone
find_actual_new = """    private Transform FindActualWallBone(Transform hit)
    {
        if (visualCache.ContainsKey(hit)) return hit;
        
        Transform current = hit.parent;
        while (current != null)
        {
            if (visualCache.ContainsKey(current)) return current;
            current = current.parent;
        }
        return null;
    }"""
text = re.sub(r'private Transform FindActualWallBone\(Transform hit\)[\s\S]*?(?=\n    // -{50}\n    // FALLING)', find_actual_new, text)

# Optimize OnBoneCrushed (remove activeBones.Remove)
text = text.replace('activeBones.Remove(bone);', '// activeBones.Remove(bone); // O(N) removed\n        visualCache.Remove(bone);')

# Optimize RebuildMatrices
rebuild_new = """    private System.Collections.Generic.List<Matrix4x4>[] groups;
    private System.Collections.Generic.List<Vector4>[] groupsColors;
    private readonly System.Collections.Generic.List<DrawBatch> batchPool = new System.Collections.Generic.List<DrawBatch>();

    private void RebuildMatrices()
    {
        drawBatches.Clear();
        if (brickMaterials == null) return;

        if (groups == null)
        {
            groups = new System.Collections.Generic.List<Matrix4x4>[MATERIAL_VARIANTS];
            groupsColors = new System.Collections.Generic.List<Vector4>[MATERIAL_VARIANTS];
            for (int i = 0; i < MATERIAL_VARIANTS; i++)
            {
                groups[i] = new System.Collections.Generic.List<Matrix4x4>(4096);
                groupsColors[i] = new System.Collections.Generic.List<Vector4>(4096);
            }
        }

        for (int i = 0; i < MATERIAL_VARIANTS; i++)
        {
            groups[i].Clear();
            groupsColors[i].Clear();
        }

        for (int i = 0; i < activeBones.Count; i++)
        {
            Transform bone = activeBones[i];
            if (bone == null) continue;
            if (bone.localScale.sqrMagnitude < 0.01f) continue;

            BlockVisual visual;
            if (!visualCache.TryGetValue(bone, out visual))
                visual = GetBlockVisual(bone);

            Matrix4x4 matrix = Matrix4x4.TRS(visual.position, visual.rotation, visual.scale);
            groups[visual.materialIndex].Add(matrix);
            groupsColors[visual.materialIndex].Add(visual.color);
        }

        int poolIndex = 0;
        for (int material = 0; material < MATERIAL_VARIANTS; material++)
        {
            System.Collections.Generic.List<Matrix4x4> source = groups[material];
            System.Collections.Generic.List<Vector4> sourceColors = groupsColors[material];

            int offset = 0;
            while (offset < source.Count)
            {
                int count = Mathf.Min(1023, source.Count - offset);
                
                DrawBatch batch;
                if (poolIndex < batchPool.Count)
                {
                    batch = batchPool[poolIndex];
                }
                else
                {
                    batch = new DrawBatch();
                    batch.matrices = new Matrix4x4[1023];
                    batch.colors = new Vector4[1023];
                    batchPool.Add(batch);
                }
                poolIndex++;

                source.CopyTo(offset, batch.matrices, 0, count);
                sourceColors.CopyTo(offset, batch.colors, 0, count);

                batch.materialIndex = material;
                batch.count = count;

                drawBatches.Add(batch);
                offset += count;
            }
        }
    }"""
text = re.sub(r'private void RebuildMatrices\(\)[\s\S]*?(?=\n    // -{50}\n    // DRAW)', rebuild_new, text)

with open(voxel_path, 'w', encoding='utf-8') as f:
    f.write(text)

print('Patched VoxelWallConverter.cs')
