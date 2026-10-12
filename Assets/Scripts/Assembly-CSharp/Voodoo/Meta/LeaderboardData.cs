using UnityEngine;
using UnityEngine.Serialization;
using Voodoo.Pattern.Random;

namespace Voodoo.Meta
{
	[CreateAssetMenu(menuName = "Voodoo/Leaderboard/LeaderboardData", fileName = "LeaderboardData")]
	public class LeaderboardData : ScriptableObject
	{
		[Tooltip("The prefab to use to create the Leaderboard's slots")]
		public LeaderboardPlayerSlot slotPrefab;

		[Tooltip("Numbers of player slots to instantiate. This won't automatically update the Leaderboard's height. Change the count for visual consistency.")]
		public int slotCount;

		[Tooltip("Player position based on slot count : 5 => player will be shown at the fifth position")]
		public int playerSlotIndex;

		[FormerlySerializedAs("margin")]
		[Tooltip("The spacing between each slot")]
		public float spacing;

		[Tooltip("Fake players names are fetched from this pool")]
		public PseudoPool pseudoPool;

		[Tooltip("Fake players flags are fetched from this pool")]
		public FlagPool flagPool;
	}
}
