namespace UnityEngine.UI.ProceduralImage
{
	public struct ProceduralImageInfo
	{
		public float width;

		public float height;

		public float fallOffDistance;

		public Vector4 radius;

		public float borderWidth;

		public float pixelSize;

		public ProceduralImageInfo(float width, float height, float fallOffDistance, float pixelSize, Vector4 radius, float borderWidth)
		{
			this.width = 0f;
			this.height = 0f;
			this.fallOffDistance = 0f;
			this.radius = default(Vector4);
			this.borderWidth = 0f;
			this.pixelSize = 0f;
		}
	}
}
