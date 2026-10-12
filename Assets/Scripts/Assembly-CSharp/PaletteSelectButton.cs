using UnityEngine;
using UnityEngine.UI;

public class PaletteSelectButton : MonoBehaviour
{
	[SerializeField]
	private PaletteScriptable palette;

	[SerializeField]
	private int PaletteID;

	private int price;

	private bool isRV;

	[SerializeField]
	private GameObject RVIcon;

	[SerializeField]
	private Button PurchaseBtn;

	private PreSetColorController colorController;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnPalettePurchased(bool withRV)
	{
	}

	public void OnButtonClicked()
	{
	}
}
