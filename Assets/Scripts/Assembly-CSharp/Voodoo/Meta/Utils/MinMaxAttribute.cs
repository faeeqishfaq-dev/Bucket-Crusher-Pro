using UnityEngine;

namespace Voodoo.Meta.Utils
{
	public class MinMaxAttribute : PropertyAttribute
	{
		public float maxLimit;

		public float minLimit;

		public bool showDebugValues;

		public bool showEditRange;

		public MinMaxAttribute(int min, int max)
		{
		}
	}
}
