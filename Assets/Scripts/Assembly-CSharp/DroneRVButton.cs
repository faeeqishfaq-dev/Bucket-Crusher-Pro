using TMPro;
using UnityEngine;

public class DroneRVButton : MonoBehaviour
{
	public GameObject Root;

	public TextMeshProUGUI Text;

	public Item DroneItem;

	private bool _available;

	private float _startOil;

	private bool _used;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	public void OnClick()
	{
		OnRVDone(true);
	}

	public void OnRVDone(bool success)
	{
	}

	private void OnEnable()
	{
	}
}
