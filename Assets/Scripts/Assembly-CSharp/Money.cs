using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.UI;
using Voodoo.Pattern;

public class Money : SingletonMB<Money>
{
	[SerializeField]
	private Text text;

	private int _lastMoney;

	private void Start()
	{
		if (text == null) text = GetComponent<Text>();
		if (text == null) text = GetComponentInChildren<Text>();
		UpdateValue();
	}

	public void UpdateValue()
	{
		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		float m = gp != null ? gp.Money() : PlayerPrefs.GetFloat("Money", 0f);
		if (text != null)
		{
			text.text = Mathf.FloorToInt(m).ToString();
		}
	}

	private void Update()
	{
	}
}
