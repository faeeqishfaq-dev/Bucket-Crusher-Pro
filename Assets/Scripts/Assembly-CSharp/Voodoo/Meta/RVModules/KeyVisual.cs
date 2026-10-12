using UnityEngine;
using UnityEngine.UI;

namespace Voodoo.Meta.RVModules
{
	public class KeyVisual : MonoBehaviour
	{
		public Image image;

		public Sprite keyEnable;

		public Sprite keyDisable;

		public Color disableColor;

		public Image fadeLight;

		public bool isKeyEnable;

		public bool CanBeEnable()
		{
			return false;
		}

		public void Enable()
		{
		}

		public bool CanBeDisable()
		{
			return false;
		}

		public void Disable()
		{
		}

		private void CheckImage()
		{
		}

		public void Dispose()
		{
		}
	}
}
