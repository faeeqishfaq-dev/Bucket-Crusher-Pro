using System.Collections.Generic;
using UnityEngine;

namespace Voodoo.Pattern.Random
{
	[CreateAssetMenu(fileName = "FlagPool", menuName = "Voodoo/Randomize/FlagPool", order = 1)]
	public class FlagPool : ScriptableObject
	{
		public List<Sprite> flags;
	}
}
