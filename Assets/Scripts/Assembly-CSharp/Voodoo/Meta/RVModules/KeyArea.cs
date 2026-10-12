using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Meta.RVModules
{
	public class KeyArea : MonoBehaviour
	{
		public KeyVisual keyPrefab;

		private List<KeyVisual> keys;

		public void SetKeys(int numberOfKey)
		{
		}

		public bool UseAKey()
		{
			return false;
		}

		public bool IsAKeyAvailable()
		{
			return false;
		}

		public void Dispose()
		{
		}
	}
}
