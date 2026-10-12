using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	private ASMRSwicher aSMRSwicher;

	private GameProperties gameProperties;

	[SerializeField]
	private Button GDPR;

	[SerializeField]
	private Button Restore;

	[SerializeField]
	private GameObject restoreFailPanel;

	[SerializeField]
	private GameObject restoreSuccessPanel;

	[SerializeField]
	private GameObject restorePanel;

	[SerializeField]
	private GameObject SettingsPanel;

	[SerializeField]
	private Button soundSwicherButton;

	[SerializeField]
	private Button hapticSwicherButton;

	private void OnEnable()
	{
	}

	private void Start()
	{
	}

	public void SoundClicked()
	{
	}

	public void HapticClicked()
	{
	}
}
