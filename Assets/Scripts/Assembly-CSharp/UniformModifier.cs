using UnityEngine;
using UnityEngine.UI.ProceduralImage;

[ModifierID("Uniform")]
public class UniformModifier : ProceduralImageModifier
{
	[SerializeField]
	private float radius;

	public float Radius
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public override Vector4 CalculateRadius(Rect imageRect)
	{
		return default(Vector4);
	}
}
