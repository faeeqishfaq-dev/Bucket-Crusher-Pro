using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Meta
{
	[CreateAssetMenu(menuName = "Voodoo/Leaderboard/LeaderboardMedalData", fileName = "LeaderboardMedalData")]
	public class LeaderboardMedalData : ScriptableObject
	{
		[Tooltip("Gives a medal based on rank: ]min,max]")]
		[SerializeField]
		private List<Vector2Int> _ranges;

		[Tooltip("The medal sprites to use")]
		[SerializeField]
		private Sprite _medalSprite;

		public Sprite MedalSprite => null;

		public bool IsInRange(int rank)
		{
			return false;
		}
	}
}
