using UnityEngine;
using UnityEngine.UI.ProceduralImage;

[ModifierID("Free")]
public class FreeModifier : ProceduralImageModifier
{
	[SerializeField]
	private Vector4 radius;

	public Vector4 Radius
	{
		get
		{
			return default(Vector4);
		}
		set
		{
		}
	}

	public override Vector4 CalculateRadius(Rect imageRect)
	{
		return default(Vector4);
	}

	protected void OnValidate()
	{
	}
}
