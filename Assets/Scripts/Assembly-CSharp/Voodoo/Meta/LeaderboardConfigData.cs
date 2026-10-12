using UnityEngine;
using UnityEngine.Serialization;

namespace Voodoo.Meta
{
	[CreateAssetMenu(menuName = "Voodoo/Leaderboard/LeaderboardConfigData", fileName = "LeaderboardConfigData")]
	public class LeaderboardConfigData : ScriptableObject
	{
		[FormerlySerializedAs("skip")]
		[Tooltip("If true, the leaderboard animation will be skipped.")]
		public bool allowAnimationSkip;

		[Tooltip("If the leaderboard animation can be skipped, a skip button will be shown, this is the delay before showing it. In seconds.")]
		public float delaySkipBtn;

		[Header("Optional")]
		[Tooltip("Sound effect to play during animation. Can be null.")]
		public AudioClip rollSFX;

		[Tooltip("Sound effect to play when user's final ranking is displayed. Can be null.")]
		public AudioClip endSFXOneShot;

		[Tooltip("Animation progression threshold to trigger a vibration. With 0.1f, you will have 10 vibrations. With 1f you will have 1 vibration.")]
		[Range(0.05f, 1f)]
		public float vibrationThreshold;
	}
}
