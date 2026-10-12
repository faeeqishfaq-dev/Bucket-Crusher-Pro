using UnityEngine;

namespace Voodoo.Meta
{
	[RequireComponent(typeof(Leaderboard))]
	public class LeaderboardAudio : MonoBehaviour
	{
		[SerializeField]
		private AudioSource _audioSource;

		public void Play(AudioClip clip)
		{
		}

		public void PlayOneShot(AudioClip clip)
		{
		}

		public void Stop()
		{
		}
	}
}
