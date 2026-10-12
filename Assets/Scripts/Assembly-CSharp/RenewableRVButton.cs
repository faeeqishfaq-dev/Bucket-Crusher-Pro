using UnityEngine;

public class RenewableRVButton : MonoBehaviour
{
	public GameObject _Button;

	private void Start()
	{
	}

	private void OnGameEnd()
	{
	}

	public void OnRVClick()
	{
		if (GameManager.Instance != null) GameManager.Instance.ActivateRenewable();
	}
}
