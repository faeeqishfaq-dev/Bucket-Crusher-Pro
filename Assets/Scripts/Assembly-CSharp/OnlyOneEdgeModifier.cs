using UnityEngine;
using UnityEngine.UI.ProceduralImage;

[ModifierID("Only One Edge")]
public class OnlyOneEdgeModifier : ProceduralImageModifier
{
	public enum ProceduralImageEdge
	{
		Top = 0,
		Bottom = 1,
		Left = 2,
		Right = 3
	}

	[SerializeField]
	private float radius;

	[SerializeField]
	private ProceduralImageEdge side;

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

	public ProceduralImageEdge Side
	{
		get
		{
			return default(ProceduralImageEdge);
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
