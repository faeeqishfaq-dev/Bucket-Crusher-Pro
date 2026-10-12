using UnityEngine;

public class DuelModeStartButtonSwitcher : MonoBehaviour
{
	public GameObject NormalButton;

	public GameObject DuelButton;

	private void Awake()
	{
		Switch();
	}

	private void Start()
	{
		Switch();
	}

	private void OnEnable()
	{
		Switch();
	}

	public void Switch()
	{
		if (NormalButton != null)
		{
			NormalButton.SetActive(true);
		}
		if (DuelButton != null)
		{
			// Only show duel button if duel mode is active/unlocked
			DuelButton.SetActive(false);
		}
	}
}
