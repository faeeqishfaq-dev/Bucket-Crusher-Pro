using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Meta
{
	[CreateAssetMenu(menuName = "Voodoo/Leaderboard/LeaderboardRankColorData", fileName = "LeaderboardRankColorData")]
	public class LeaderboardRankColorData : ScriptableObject
	{
		[Tooltip("Define a range to apply color scheme based on rank: ]min,max]")]
		public List<Vector2Int> rankRanges;

		[Tooltip("The color for the background of a non player slot.")]
		public Color backgroundColor;

		[Tooltip("The color for the rank's background of a non player slot.")]
		public Color rankBackgroundColor;

		[Tooltip("The color for the background of the player slot.")]
		public Color playerBackgroundColor;

		[Tooltip("The color for the rank's background of the player slot.")]
		public Color playerRankBackgroundColor;

		public bool IsRankInRange(int rank)
		{
			return false;
		}
	}
}
