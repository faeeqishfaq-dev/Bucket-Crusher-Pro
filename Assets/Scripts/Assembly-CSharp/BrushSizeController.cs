using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BrushSizeController : MonoBehaviour
{
	[SerializeField]
	private Image m_FillBar;

	private const int c_MinSize = 5;

	private const int c_MaxSize = 25;

	public static event Action<float> OnValueChangedEvent
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

	private void OnDestroy()
	{
	}

	private void OnColorButtonClicked(Color _Color)
	{
	}

	public void OnValueChanged(float _Value)
	{
	}
}
