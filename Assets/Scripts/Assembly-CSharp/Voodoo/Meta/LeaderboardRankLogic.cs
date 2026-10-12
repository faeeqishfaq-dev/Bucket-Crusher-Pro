using UnityEngine;

namespace Voodoo.Meta
{
	public class LeaderboardRankLogic
	{
		private readonly LeaderboardRankData _data;

		public int PlayerRank { get; private set; }

		public int PreviousRank { get; private set; }

		public int NewRank { get; private set; }

		public int RankStep { get; private set; }

		public int PreviousScore { get; private set; }

		public int NewScore { get; private set; }

		public bool RankUp { get; private set; }

		public LeaderboardRankLogic(LeaderboardRankData data)
		{
		}

		public void Initialize(int previousScore, int newScore)
		{
		}

		public int GetPlayerRank()
		{
			return 0;
		}

		public int GetNextRank(int previousScore, int newScore)
		{
			return 0;
		}

		public int GetNextRankFromStep(int previousScore, int newScore, int rankStep)
		{
			return 0;
		}

		public LeaderboardBackgroundColors GetPlayerBackgroundsColors()
		{
			return default(LeaderboardBackgroundColors);
		}

		public LeaderboardBackgroundColors GetBackgroundColorsByRank(int rank, bool isPlayer = false)
		{
			return default(LeaderboardBackgroundColors);
		}

		public Sprite GetPlayerMedal()
		{
			return null;
		}

		public Sprite GetMedalByRank(int rank)
		{
			return null;
		}

		public void SaveNewRank()
		{
		}

		public int GetRankEarned(int newScore)
		{
			return 0;
		}
	}
}
