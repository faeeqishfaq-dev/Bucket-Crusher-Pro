import re

# 1. Fix VoxelWallConverter.cs
voxel = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/VoxelWallConverter.cs'
with open(voxel, 'r', encoding='utf-8') as f:
    text = f.read()

voxel_update_new = """    private MaterialPropertyBlock propBlock;
    private void Update()
    {
        if (isDirty)
        {
            RebuildMatrices();
            isDirty = false;
        }

        if (activeBones.Count > 0 && drawBatches.Count > 0)
        {
            if (smr != null && smr.enabled) smr.enabled = false;
        }

        if (cubeMesh == null || brickMaterials == null) return;

        if (propBlock == null) propBlock = new MaterialPropertyBlock();

        for (int i = 0; i < drawBatches.Count; i++)
        {
            DrawBatch batch = drawBatches[i];
            Material material = brickMaterials[batch.materialIndex];
            if (material == null) continue;

            propBlock.Clear();
            if (batch.colors != null)
                propBlock.SetVectorArray(ColorPropertyID, batch.colors);

            Graphics.DrawMeshInstanced(
                cubeMesh, 0, material, batch.matrices, batch.count,
                propBlock, UnityEngine.Rendering.ShadowCastingMode.On, true, gameObject.layer
            );
        }
    }"""
text = re.sub(r'private void Update\(\)[\s\S]*?(?=\n    // -{50}\n    // FALLING)', voxel_update_new, text)
with open(voxel, 'w', encoding='utf-8') as f:
    f.write(text)

# 2. Fix Progress.cs
prog = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Progress.cs'
with open(prog, 'r', encoding='utf-8') as f:
    text = f.read()

# Add Update() for throttled UI
prog_update = """
	private bool _isUiDirty = false;
	private void Update()
	{
		if (_isUiDirty)
		{
			UpdateUI();
			_isUiDirty = false;
		}
	}

	public void KillBlock"""
text = text.replace('public void KillBlock', prog_update)

# Replace UpdateUI() calls in KillBlock with _isUiDirty = true;
# Actually, I can just replace all UpdateUI() with _isUiDirty = true;
text = text.replace('UpdateUI();', '_isUiDirty = true;')
# But wait, in Start() or ResetCount() we might want immediate update. It's fine to just set dirty.
# Let's manually replace in KillBlock
with open(prog, 'w', encoding='utf-8') as f:
    f.write(text)

# 3. Fix Oil.cs
oil = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs'
with open(oil, 'r', encoding='utf-8') as f:
    text = f.read()

# Remove PlayerPrefs from Update
text = text.replace('PlayerPrefs.SetFloat("SavedOilValue", CurrentOilValue);', '// Saved in OnDisable')

# Add OnDisable
oil_disable = """	private void OnDisable()
	{
		PlayerPrefs.SetFloat("SavedOilValue", CurrentOilValue);
	}

	void Update()"""
text = text.replace('void Update()', oil_disable)
with open(oil, 'w', encoding='utf-8') as f:
    f.write(text)

print('Patched Voxel, Progress, Oil')
