using UnityEngine;

namespace Voodoo.Meta
{
	public struct LeaderboardBackgroundColors
	{
		public Color backgroundColor;

		public Color rankBackgroundColor;

		public LeaderboardBackgroundColors(Color backgroundColor, Color rankBackgroundColor)
		{
			this.backgroundColor = default(Color);
			this.rankBackgroundColor = default(Color);
		}
	}
}
