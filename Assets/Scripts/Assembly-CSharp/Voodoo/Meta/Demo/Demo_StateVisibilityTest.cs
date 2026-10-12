using UnityEngine;

namespace Voodoo.Meta.Demo
{
	[RequireComponent(typeof(Canvas))]
	public class Demo_StateVisibilityTest : MonoBehaviour
	{
		[SerializeField]
		private Demo_LeaderboardTest _leaderboardTest;

		[SerializeField]
		private Demo_LeaderboardTest.GameState _activeState;

		private Canvas _canvas;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnValidate()
		{
		}

		private void OnStateChanged(Demo_LeaderboardTest.GameState newState)
		{
		}

		private void SetVisiblity(bool isVisible)
		{
		}
	}
}
