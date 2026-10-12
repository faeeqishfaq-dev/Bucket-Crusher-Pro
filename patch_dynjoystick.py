import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/DynamicJoystick.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

pointer_down_original = """	public override void OnPointerDown(PointerEventData eventData)
	{
		if (isPointerDown) return;
		if (background != null)"""

pointer_down_fixed = """	public override void OnPointerDown(PointerEventData eventData)
	{
		if (isPointerDown) return;

		// UI Touch Guard
		if (EventSystem.current != null)
		{
			bool isOverUI = false;
#if UNITY_EDITOR || UNITY_STANDALONE
			if (EventSystem.current.IsPointerOverGameObject()) isOverUI = true;
#else
			if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) isOverUI = true;
#endif
			if (isOverUI)
			{
				var results = new System.Collections.Generic.List<RaycastResult>();
				EventSystem.current.RaycastAll(eventData, results);
				if (results.Count > 0 && results[0].gameObject != gameObject)
				{
					return; // DO NOT start joystick
				}
			}
		}

		if (background != null)"""

text = text.replace(pointer_down_original, pointer_down_fixed)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Fixed DynamicJoystick.cs")
