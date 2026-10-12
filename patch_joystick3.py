import re

file_path = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Joystick.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    text = f.read()

# Replace the OnPointerDown
old_pd = re.search(r'public virtual void OnPointerDown.*?OnDrag\(eventData\);\n\t}', text, re.DOTALL).group(0)

clean_pd = '''	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (isPointerDown) return;

		if (IsPointerOverMultiplierButton(eventData.position)) return;

		isPointerDown = true;
		activePointerId = eventData.pointerId;
		OnDrag(eventData);
	}'''
text = text.replace(old_pd, clean_pd)

old_dir = re.search(r'// UI Touch Guard for fallback.*?if \(Input\.touchCount > 0\)', text, re.DOTALL)
if old_dir:
    text = text.replace(old_dir.group(0), 'if (Input.touchCount > 0)')

clean_dir = '''			// Direct touch fallback (for mobile Device Simulator or touch screen if pointer event was missed)
			if (Input.touchCount > 0)
			{
				Touch t = Input.GetTouch(0);
				if (IsPointerOverMultiplierButton(t.position)) return Vector2.zero;
				if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
'''
text = text.replace('''			// Direct touch fallback (for mobile Device Simulator or touch screen if pointer event was missed)
			if (Input.touchCount > 0)
			{
				Touch t = Input.GetTouch(0);
				if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)''', clean_dir)

# Also patch the mouse fallback!
old_mouse = '''			// Mouse fallback
			if (Input.GetMouseButton(0))
			{'''
new_mouse = '''			// Mouse fallback
			if (Input.GetMouseButton(0))
			{
				if (IsPointerOverMultiplierButton(Input.mousePosition)) return Vector2.zero;'''
text = text.replace(old_mouse, new_mouse)

helper_method = '''
	protected bool IsPointerOverMultiplierButton(Vector2 screenPos)
	{
		if (EventSystem.current == null) return false;
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		eventData.position = screenPos;
		var results = new System.Collections.Generic.List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, results);
		for (int i = 0; i < results.Count; i++)
		{
			Transform t = results[i].gameObject.transform;
			while (t != null)
			{
				if (t.name == "ExcavatorMultiplier" || t.name == "Button")
				{
					return true;
				}
				t = t.parent;
			}
		}
		return false;
	}
}
'''
text = re.sub(r'}\s*$', helper_method, text)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(text)

print('Joystick.cs patched.')
