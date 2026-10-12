using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Voodoo.Meta.Utils;

namespace Voodoo.Meta
{
	[CreateAssetMenu(menuName = "Voodoo/Leaderboard/LeaderboardRankData", fileName = "LeaderboardRankData")]
	public class LeaderboardRankData : ScriptableObject
	{
		[Tooltip("The starting rank of the player.")]
		public int playerStartRank;

		[Tooltip("The animation curve describes the number of ranks gained for a defined score.")]
		public AnimationCurve rankCurve;

		[Tooltip("If true the rank step will be clamped using rankClampRange")]
		public bool allowRankClamp;

		[Tooltip("The rank step gained by the player is clamp in a min/max range.")]
		[MinMax(0, 1000, showEditRange = true)]
		public Vector2Int rankClampRange;

		[FormerlySerializedAs("_colorDatas")]
		[Tooltip("A list of color scheme to apply to the leaderboard slots based on rank.")]
		public List<LeaderboardRankColorData> colorData;

		[FormerlySerializedAs("_leaderboardMedalDatas")]
		[Tooltip("A list of medal to give if a slot is in a specific range.")]
		public List<LeaderboardMedalData> medalData;

		public LeaderboardBackgroundColors GetBackgroundColorsByRank(int rank, bool player = false)
		{
			return default(LeaderboardBackgroundColors);
		}

		public Sprite GetMedalByRank(int rank)
		{
			return null;
		}

		public int GetRankForScore(int score)
		{
			return 0;
		}
	}
}
