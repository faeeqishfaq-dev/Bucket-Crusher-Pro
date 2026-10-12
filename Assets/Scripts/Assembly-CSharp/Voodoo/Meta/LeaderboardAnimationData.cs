using UnityEngine;

namespace Voodoo.Meta
{
	[CreateAssetMenu(menuName = "Voodoo/Leaderboard/LeaderboardAnimationData", fileName = "LeaderboardAnimationData")]
	public class LeaderboardAnimationData : ScriptableObject
	{
		[Tooltip("The duration of the player' slot scale up animation")]
		public float playerUpScaleDuration;

		[Tooltip("The factor by which the player slot will be scaled up")]
		public float playerUpScaleFactor;

		[Tooltip("The duration of the player slot down scale")]
		public float playerDownScaleDuration;

		[Tooltip("The duration of the main animation")]
		public float duration;

		[Tooltip("The waiting time before closing the leaderboard after everything else")]
		public float waitingTime;

		public int WaitingTimeInMilliSeconds => 0;
	}
}
