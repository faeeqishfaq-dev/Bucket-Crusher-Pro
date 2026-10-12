using UnityEngine;

namespace Voodoo.Pattern
{
	public class SingletonMB<T> : MonoBehaviour
	{
		private static T _instance;
		private static readonly object _lock = new object();
		private static bool _isQuitting;

		public static T Instance
		{
			get
			{
				if (_isQuitting && !Application.isPlaying)
				{
					return default;
				}

				lock (_lock)
				{
					if (_instance == null || (_instance as UnityEngine.Object) == null)
					{
						var found = Object.FindAnyObjectByType(typeof(T));
						if (found != null)
						{
							_instance = (T)(object)found;
						}
					}
					return _instance;
				}
			}
		}

		public void SetActive(bool active)
		{
			gameObject.SetActive(active);
		}

		protected virtual void OnDestroySpecific()
		{
		}

		protected virtual void OnApplicationQuit()
		{
			_isQuitting = true;
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetStatics()
		{
			_instance = default;
			_isQuitting = false;
		}
	}
}
