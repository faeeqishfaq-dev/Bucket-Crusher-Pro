namespace UnityEngine.UI.ProceduralImage
{
	[DisallowMultipleComponent]
	public abstract class ProceduralImageModifier : MonoBehaviour
	{
		protected Graphic graphic;

		protected Graphic _Graphic => null;

		public abstract Vector4 CalculateRadius(Rect imageRect);
	}
}
