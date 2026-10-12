using UnityEngine;
using UnityEngine.UI;

namespace Voodoo.Meta.Demo
{
	[RequireComponent(typeof(Demo_LeaderboardTest))]
	public class Demo_RestartLeaderboard : MonoBehaviour
	{
		public int scoreMultiplier;

		public Button restartButton;

		private Leaderboard _leaderboard;

		private Demo_LeaderboardTest _leaderboardTest;

		private void Start()
		{
		}

		public void RestartWithBonus()
		{
		}

		public void SetRestartButtonVisibility(bool isActive)
		{
		}
	}
}
