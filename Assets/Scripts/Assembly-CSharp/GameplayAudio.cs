using System.Collections.Generic;
using UnityEngine;

public class GameplayAudio : MonoBehaviour, ISwichSound
{
	private List<AudioSource> _audioSources;

	private List<AudioClip> _audioClips;

	[SerializeField]
	private AudioClip[] _digSounds;

	[SerializeField]
	private AudioClip[] _scratchSounds;

	[SerializeField]
	private AudioClip _coinCollect;

	[SerializeField]
	private AudioClip _upgrade;

	[SerializeField]
	private AudioSource _machineAudioSource;

	[SerializeField]
	private MoveDirection _joystick;

	private int _currentDigSounds;

	private int _currentScratchSounds;

	private int _currentCoinSounds;

	private float _scratchSoundTimer;

	private float _digSoundTimer;

	private float _coinTimer;

	private bool _canPlay = true;

	private void Awake()
	{
		_canPlay = PlayerPrefs.GetInt("Sound", 1) == 1;
	}

	private void Start()
	{
		if (_machineAudioSource == null)
		{
			_machineAudioSource = GetComponent<AudioSource>();
		}
	}

	internal void ReplaceJoystick(MoveDirection joystick)
	{
		_joystick = joystick;
	}

	private void FixedUpdate()
	{
	}

	public void PlaySound(GameplaySoundType soundType)
	{
		if (!_canPlay) return;
		switch (soundType)
		{
			case GameplaySoundType.DigSound:
				DigSound();
				break;
			case GameplaySoundType.Scratch:
				ScratchSound();
				break;
			case GameplaySoundType.Coin:
				CoinCollectedSound();
				break;
			case GameplaySoundType.Upgrade:
				UpgradeShownSound();
				break;
		}
	}

	private void DigSound()
	{
		if (!_canPlay) return;
		if (Time.time < _digSoundTimer) return;
		_digSoundTimer = Time.time + 0.1f;
		if (_digSounds != null && _digSounds.Length > 0)
		{
			AudioClip clip = _digSounds[Random.Range(0, _digSounds.Length)];
			if (clip != null) AudioSource.PlayClipAtPoint(clip, Camera.main != null ? Camera.main.transform.position : transform.position);
		}
	}

	private void ScratchSound()
	{
		if (!_canPlay) return;
		if (Time.time < _scratchSoundTimer) return;
		_scratchSoundTimer = Time.time + 0.15f;
		if (_scratchSounds != null && _scratchSounds.Length > 0)
		{
			AudioClip clip = _scratchSounds[Random.Range(0, _scratchSounds.Length)];
			if (clip != null) AudioSource.PlayClipAtPoint(clip, Camera.main != null ? Camera.main.transform.position : transform.position);
		}
	}

	public void CoinCollectedSound()
	{
		if (!_canPlay) return;
		if (Time.time < _coinTimer) return;
		_coinTimer = Time.time + 0.1f;
		if (_coinCollect != null)
		{
			AudioSource.PlayClipAtPoint(_coinCollect, Camera.main != null ? Camera.main.transform.position : transform.position);
		}
	}

	private void UpgradeShownSound()
	{
		if (!_canPlay) return;
		if (_upgrade != null)
		{
			AudioSource.PlayClipAtPoint(_upgrade, Camera.main != null ? Camera.main.transform.position : transform.position);
		}
	}

	public void SoundOff()
	{
		_canPlay = false;
	}

	public void SoundOn()
	{
		_canPlay = true;
	}

	internal void PlaySound(AudioClip clip)
	{
		if (_canPlay && clip != null)
		{
			AudioSource.PlayClipAtPoint(clip, Camera.main != null ? Camera.main.transform.position : transform.position);
		}
	}
}
