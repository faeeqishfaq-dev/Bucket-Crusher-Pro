using UnityEngine;

namespace Voodoo.Meta.RVModules
{
	public class KeyCollectable : MonoBehaviour
	{
		private const string _collectTrigger = "Collect";

		private const string _idleAnimation = "KeyIdle";

		public Collider keyCollider;

		public GameObject particleGetKey;

		public GameObject particleGlow;

		public GameObject particleEffect;

		public AudioClip collectSound;

		private bool _collected;

		private void Start()
		{
		}

		public void DoCollect()
		{
		}

		private void OnTriggerEnter(Collider other)
		{
		}

		public void OnDisable()
		{
		}
	}
}
