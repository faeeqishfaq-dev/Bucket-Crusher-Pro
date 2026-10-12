import re
import os

head_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Head.cs'
with open(head_path, 'r', encoding='utf-8') as f:
    text = f.read()

# Replace FixedUpdate
fixed_update_new = """	private Collider[] _hitResults = new Collider[256];
	private SphereCollider _col;

	void FixedUpdate()
	{
		if (_col == null) _col = GetComponent<SphereCollider>();
		if (_col != null)
		{
			float r = _col.radius * Mathf.Max(transform.lossyScale.x, Mathf.Max(transform.lossyScale.y, transform.lossyScale.z));
			int hits = Physics.OverlapSphereNonAlloc(transform.TransformPoint(_col.center), r, _hitResults);
			for (int i = 0; i < hits; i++)
			{
				Collider hit = _hitResults[i];
				if (hit != null && hit != _col && hit.enabled)
				{
					if (hit.transform.root != transform.root)
					{
						Trigged(hit);
					}
				}
			}
		}
	}"""
text = re.sub(r'void FixedUpdate\(\)[\s\S]*?\}[\s\S]*?\}[\s\S]*?\}[\s\S]*?\}[\s\S]*?\}[\s\S]*?\}', fixed_update_new, text)

# Replace Trigged
trigged_new = """	void Trigged(Collider other)
	{
		if (other == null || !other.enabled) return;

		// Optimize: use layer 10
		if (other.gameObject.layer == 10)
		{
			AddPhysics(other);

			if (progress != null)
			{
				progress.KillBlock();
			}
			else if (Progress.Instance != null)
			{
				Progress.Instance.KillBlock();
			}

			if (gameplayAudio != null)
			{
				gameplayAudio.PlaySound(GameplaySoundType.DigSound);
			}
			if (gameplayHaptic != null)
			{
				gameplayHaptic.PlayHaptic(HapticType.Dig);
			}
		}
	}"""
text = re.sub(r'void Trigged\(Collider other\)[\s\S]*?void AddPhysics', trigged_new + '\n\n\tvoid AddPhysics', text)

with open(head_path, 'w', encoding='utf-8') as f:
    f.write(text)

print('Patched Head.cs')
