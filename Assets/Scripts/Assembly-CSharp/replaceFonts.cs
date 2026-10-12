using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[ExecuteInEditMode]
public class replaceFonts : MonoBehaviour
{
	public Font _baseFont;

	public TMP_FontAsset _TMProFont;

	public List<GameObject> objects;

	[SerializeField]
	public List<Text> uiText;

	[SerializeField]
	public List<TextMeshProUGUI> meshText;

	[SerializeField]
	public List<TextMeshPro> meshText1;

	public List<string> path;

	public List<GameObject> o;

	[ContextMenu("Do Something")]
	public void Find()
	{
	}

	[ContextMenu("Prefab acting")]
	private void FindInPrefab()
	{
	}
}
