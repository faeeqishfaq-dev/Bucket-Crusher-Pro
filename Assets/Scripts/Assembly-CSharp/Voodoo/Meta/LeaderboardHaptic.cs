using UnityEngine;

namespace Voodoo.Meta
{
	[RequireComponent(typeof(Leaderboard))]
	public class LeaderboardHaptic : MonoBehaviour
	{
		private int _rankVibrationThreshold;

		public void Initialize(int rankVibrationThreshold)
		{
		}

		public void VibrateOnAnimationStart()
		{
		}

		public void VibrateOnAnimationEnd()
		{
		}

		public void VibrateWhenRankClimbing(int rank)
		{
		}

		private void Vibrate()
		{
		}
	}
}
