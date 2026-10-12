using System;

namespace UnityEngine.UI.ProceduralImage
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ModifierID : Attribute
	{
		private string name;

		public string Name => null;

		public ModifierID(string name)
		{
		}
	}
}
