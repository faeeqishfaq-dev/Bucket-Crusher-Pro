using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Voodoo.Meta.Demo
{
	public class Demo_LeaderboardTest : MonoBehaviour
	{
		public enum GameState
		{
			Idle = 0,
			Playing = 1,
			Leaderboard = 2
		}

		public int currentScore;

		public int minScoreRange;

		public int maxScoreRange;

		public Leaderboard leaderboard;

		public TextMeshProUGUI stateText;

		public string playerName;

		public Sprite playerFlag;

		private float _elapsedTime;

		private GameState _gameState;

		[NonSerialized]
		public int GainedScore;

		public event Action<GameState> stateChanged
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void StartGame()
		{
		}

		public void EndGame()
		{
		}

		private void ShowLeaderboard(int startScore, int newScore)
		{
		}

		private void ShowLeaderboard(int startScore, int newScore, string name, Sprite flag)
		{
		}

		private void ShowLeaderboardUsingFirst(int startScore, int newScore)
		{
		}

		private void ChangeState(GameState newState)
		{
		}
	}
}
