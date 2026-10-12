using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Voodoo.Pattern.Random
{
	[CreateAssetMenu(fileName = "PseudoPool", menuName = "Voodoo/Randomize/PseudoPool", order = 1)]
	public class PseudoPool : ScriptableObject
	{
		[FormerlySerializedAs("_Strings")]
		public List<string> _strings;

		public string GetRandomString(int _minCharacterCount = 1, int _maxCharacterCount = -1)
		{
			return null;
		}
	}
}
