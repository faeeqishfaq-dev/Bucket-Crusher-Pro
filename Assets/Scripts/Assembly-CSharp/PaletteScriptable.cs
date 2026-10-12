using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPalette", menuName = "New Palette")]
public class PaletteScriptable : ScriptableObject
{
	public List<Color> colors;
}
