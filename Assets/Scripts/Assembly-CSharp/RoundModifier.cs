using UnityEngine;
using UnityEngine.UI.ProceduralImage;

[ModifierID("Round")]
public class RoundModifier : ProceduralImageModifier
{
	public override Vector4 CalculateRadius(Rect imageRect)
	{
		return default(Vector4);
	}
}
