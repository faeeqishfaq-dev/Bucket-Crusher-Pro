import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Joystick.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

# Add UI check to OnPointerDown
pointer_down_original = """	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (isPointerDown) return;
		isPointerDown = true;
		activePointerId = eventData.pointerId;
		OnDrag(eventData);
	}"""

pointer_down_fixed = """	public virtual void OnPointerDown(PointerEventData eventData)
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
				// Check if it's over a different UI element (like the ExcavatorMultiplier button)
				var results = new System.Collections.Generic.List<RaycastResult>();
				EventSystem.current.RaycastAll(eventData, results);
				if (results.Count > 0 && results[0].gameObject != gameObject)
				{
					return; // DO NOT start joystick
				}
			}
		}

		isPointerDown = true;
		activePointerId = eventData.pointerId;
		OnDrag(eventData);
	}"""

text = text.replace(pointer_down_original, pointer_down_fixed)

# Add UI check to Direction fallback
direction_original = """			// Direct touch fallback (for mobile Device Simulator or touch screen if pointer event was missed)
			if (Input.touchCount > 0)"""

direction_fixed = """			// UI Touch Guard for fallback
			if (EventSystem.current != null)
			{
				bool isOverUI = false;
#if UNITY_EDITOR || UNITY_STANDALONE
				if (EventSystem.current.IsPointerOverGameObject()) isOverUI = true;
#else
				if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) isOverUI = true;
#endif
				if (isOverUI) return Vector2.zero;
			}

			// Direct touch fallback (for mobile Device Simulator or touch screen if pointer event was missed)
			if (Input.touchCount > 0)"""

text = text.replace(direction_original, direction_fixed)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print("Fixed Joystick.cs")
