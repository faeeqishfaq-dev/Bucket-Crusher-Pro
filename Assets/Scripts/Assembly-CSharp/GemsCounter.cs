using UnityEngine;
using UnityEngine.UI;
using Voodoo.Pattern;

public class GemsCounter : SingletonMB<GemsCounter>
{
	[SerializeField]
	private Text text;

	private void Start()
	{
	}

	public void UpdateValue()
	{
	}
}
