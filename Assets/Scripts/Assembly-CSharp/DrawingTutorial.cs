using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Voodoo.Utils;

public class DrawingTutorial : MonoBehaviour
{
	private Animator m_Animator;

	private const string c_HasDrawingTutorialBeenDisplayedKey = "HasDrawingTutorialBeenDisplayedKey";

	[SerializeField]
	private TextMeshProUGUI m_SkipText;

	[SerializeField]
	private Image m_BackgroundImage;

	private DelayAction hideTutorialDelayed;

	public bool HasDrawingTutorialBeenDisplayed
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SkipTutorialButtonClicked()
	{
	}

	private void HideTutorial()
	{
	}
}
