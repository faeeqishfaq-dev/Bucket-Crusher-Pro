using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
	[SerializeField]
	private Image m_ButtonImage;

	[SerializeField]
	private Shadow m_ButtonShadowImage;

	private Button m_Button;

	public Color Color => default(Color);

	public static event Action<Color> OnColorButtonClicked
	{
		[CompilerGenerated]
		add
		{
		}
		[CompilerGenerated]
		remove
		{
		}
	}

	private void Awake()
	{
	}

	public void Init(Color _Color)
	{
	}

	private void OnClick()
	{
	}
}
