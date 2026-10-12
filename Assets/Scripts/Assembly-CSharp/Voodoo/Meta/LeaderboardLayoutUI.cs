using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Meta
{
	public class LeaderboardLayoutUI
	{
		private readonly LeaderboardLayout _leaderboard;

		private readonly LeaderboardData _leaderboardData;

		private readonly RectTransform _slotParent;

		private readonly LeaderboardPlayerSlot _slotPrefab;

		private readonly List<LeaderboardPlayerSlot> _slots;

		private LeaderboardPlayerInfo _playerInfo;

		public LeaderboardPlayerSlot PlayerSlot { get; private set; }

		public IReadOnlyList<LeaderboardPlayerSlot> Slots => null;

		public LeaderboardRankLogic RankLogic { get; }

		public LeaderboardLayoutUI(LeaderboardLayout leaderboard, LeaderboardRankLogic rankLogic)
		{
		}

		~LeaderboardLayoutUI()
		{
		}

		public void Fill(LeaderboardPlayerInfo playerInfo)
		{
		}

		public void MovePlayerSlotInLayoutTransform()
		{
		}

		private void ReturnPlayerSlotToOriginalTransform()
		{
		}

		public void FocusOnSlotIndex(int index)
		{
		}

		public void RefreshPlayerRank()
		{
		}

		public void MoveLastSlots()
		{
		}

		public void SwitchLastSlotIndex()
		{
		}

		private void MoveLowerRankedPlayerDown()
		{
		}

		public void Clear()
		{
		}

		private void MoveHigherRankedPlayerUp()
		{
		}

		private void MoveLastSlotAtTheTop()
		{
		}

		private void MoveFirstSlotAtTheBottom()
		{
		}

		private LeaderboardPlayerSlot Instantiate()
		{
			return null;
		}

		private void CreateSlots()
		{
		}

		private void UpdateSlots()
		{
		}

		private void InitNonPlayerSlot(LeaderboardPlayerSlot slot, Vector2 anchoredPosition, int rank)
		{
		}

		private void InitPlayerSlot(Vector2 anchoredPosition, int rank)
		{
		}

		private LeaderboardSlotInfo CreateSlotInfo(int rank, bool isPlayer)
		{
			return null;
		}

		private void OnPlayerNameChanged(string newName)
		{
		}
	}
}
