using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
	public Image icon;

	public TextMeshProUGUI CostText;

	public Text MissionText;

	public Text MaxPurchaseText;

	public GameObject LevelGroup;

	public Text LevelText;

	protected Item _item;

	public Button _button;

	public Action OnClicked;

	public Text _description;

	[Header("RV")]
	public RVGroup rVGroup;

	public bool UseRV;

	[Header("Perks")]
	public Text fuel;

	public Text power;

	public Text size;

	public Item Item => null;

	public bool useGems(Item item)
	{
		return false;
	}

	public virtual void WatchRV()
	{
	}

	public virtual void OnClick()
	{
	}

	private void ShowHideRV()
	{
	}

	public virtual void Fill(Item item, bool useCurrency = true, bool showLevel = false)
	{
	}
}
