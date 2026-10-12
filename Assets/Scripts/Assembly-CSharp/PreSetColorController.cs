using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PreSetColorController : MonoBehaviour
{
	public delegate void PalettePurchased(bool isRV);

	[SerializeField]
	private ColorButton m_ColorButtonPrefab;

	[HideInInspector]
	public List<Color> currentPalette;

	public PaletteScriptable defaultPalette;

	private ColorButton[] m_ColorButtons;

	public event PalettePurchased OnPalettePurchased
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Start()
	{
	}

	public void SetPalette(List<Color> colors)
	{
	}

	public void OnPalettePurchasedEvent(bool isRV)
	{
	}

	public void ResetPalette()
	{
	}
}
