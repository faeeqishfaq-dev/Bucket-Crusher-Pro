using System;
using UnityEngine;

public class UIAudio : MonoBehaviour, ISwichSound
{
	private AudioSource audioSource;

	private bool canPlay;

	[SerializeField]
	private AudioClip _button;

	[SerializeField]
	private AudioClip _lostFuel;

	[SerializeField]
	private AudioClip _upgrade;

	[SerializeField]
	private AudioClip _win;

	[SerializeField]
	private AudioClip _coinAppear;

	[SerializeField]
	private AudioClip _buildingUpgrade;

	[SerializeField]
	private Transform[] _canvases;

	private Action _buttonSoundAction;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void PlayButtonHaptic()
	{
	}

	public void SoundOff()
	{
	}

	public void SoundOn()
	{
	}

	private void PlayButtonSound()
	{
	}

	public void PlaySound(UISoundType soundType)
	{
	}
}
