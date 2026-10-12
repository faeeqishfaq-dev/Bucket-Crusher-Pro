using UnityEngine;
using UnityEngine.UI;

public class LostFuelScreen : MonoBehaviour
{
	[SerializeField]
	private GameObject _baseScreen;

	[SerializeField]
	private GameObject _getMoreMoneyScreen;

	[SerializeField]
	private GameObject _getMoreFuelScreen;

	[SerializeField]
	private GameObject _GetXButton;

	[SerializeField]
	private Text _earned;

	[SerializeField]
	private Text _getX;

	[SerializeField]
	private Sell _sell;

	public void FuelLost()
	{
		ShowScreen();
	}

	public void ShowScreen()
	{
		if (_baseScreen != null) _baseScreen.SetActive(true);
	}
}
