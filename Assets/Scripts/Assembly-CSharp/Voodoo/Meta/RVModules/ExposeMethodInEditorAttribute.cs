using System;

namespace Voodoo.Meta.RVModules
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ExposeMethodInEditorAttribute : Attribute
	{
		private bool _isValueChange;

		private bool _isForceExecuteInEditMode;

		public bool IsForceExecuteInEditMode => false;

		public bool IsValueChange => false;

		public ExposeMethodInEditorAttribute(bool isValueChange = false, bool isForceExecuteInEditMode = false)
		{
		}
	}
}
