using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityBuildingUpgradeUI : MonoBehaviour
{
	public GameObject CurrentState;

	public GameObject Arrow;

	public GameObject NextState;

	public TextMeshProUGUI CurrentGainText;

	public TextMeshProUGUI NextGainText;

	public TextMeshProUGUI NextCost;

	public Button UpgradeButton;

	public TextMeshProUGUI Title;

	public TextMeshProUGUI LevelText;

	public Color AvailableColor;

	public TextMeshProUGUI RVPrice;

	public GameObject RVDock;

	private CityBuildingElementHandler _currentHandler;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	public void OnCollect()
	{
	}

	public void Setup(CityBuildingElementHandler elem)
	{
	}

	public void OnUpgradeClicked()
	{
	}

	private void OnUpgradeClickedRVCallback(bool success)
	{
	}

	public void OnCloseClicked()
	{
	}
}
