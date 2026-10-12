using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.UI;
using Voodoo.Pattern;

public class Progress : SingletonMB<Progress>
{
	[SerializeField]
	private Slider _slider;

	[SerializeField]
	private Text _level;

	[SerializeField]
	private GameObject _endFlag;

	[SerializeField]
	private TMP_Text _textProgress;

	[SerializeField]
	private DuelModeProgressUI DuelModeProgress;

	private int maxCount = 6400;

	private int currentCount;

	private float step;

	private bool _moreThanThird;

	private int DestroyedByPlayer;

	private int DestroyedByOpponent;

	private int initialBlockCount = -1;

	private void Start()
	{
		GameProperties gp = Object.FindAnyObjectByType<GameProperties>();
		if (_level != null && gp != null)
		{
			_level.text = "LEVEL " + gp.Level();
		}
		if (maxCount <= 0) maxCount = 6400;
		_isUiDirty = true;
	}

	public void InitializeLevelBaseline()
	{
		if (VoxelWallConverter.Instance != null)
		{
			initialBlockCount = VoxelWallConverter.Instance.ActiveBonesCount;
		}
		else
		{
			initialBlockCount = maxCount > 0 ? maxCount : 6400;
		}
		
		currentCount = 0;
		DestroyedByPlayer = 0;
		DestroyedByOpponent = 0;
		
		_isUiDirty = true;
	}

	private float GetProgressPct()
	{
		if (initialBlockCount <= 0) return 0f;
		
		int remainingBlockCount = initialBlockCount;
		if (VoxelWallConverter.Instance != null)
		{
			remainingBlockCount = VoxelWallConverter.Instance.ActiveBonesCount;
		}
		
		int destroyedBlockCount = initialBlockCount - remainingBlockCount;
		if (destroyedBlockCount < 0) destroyedBlockCount = 0;
		
		float progress = (float)destroyedBlockCount / initialBlockCount;
		
		if (progress >= 0.995f)
		{
			return 1.0f;
		}
		
		return Mathf.Clamp01(progress);
	}

	public int Pr()
	{
		return Mathf.RoundToInt(GetProgressPct() * 100f);
	}

	public int GetMaxCount()
	{
		return maxCount;
	}

	public void SetMaxCount(int count)
	{
		maxCount = count > 0 ? count : 6400;
		_isUiDirty = true;
	}

	
			private bool _isUiDirty = false;
		private void Update()
	{
		if (_isUiDirty)
		{
			float pct = GetProgressPct();
			if (_slider != null)
			{
				_slider.value = pct;
			}
			if (_textProgress != null)
			{
				_textProgress.text = Mathf.RoundToInt(pct * 100f) + "%";
			}
			_isUiDirty = false;
		}
	}
	public void KillBlock()
	{
		currentCount++;
		DestroyedByPlayer++;
		_isUiDirty = true;

		if (initialBlockCount > 0)
		{
			int remainingBlockCount = initialBlockCount;
			if (VoxelWallConverter.Instance != null)
			{
				remainingBlockCount = VoxelWallConverter.Instance.ActiveBonesCount;
			}
			
			int destroyedBlockCount = initialBlockCount - remainingBlockCount;
			if (destroyedBlockCount < 0) destroyedBlockCount = 0;
			
			float progress = (float)destroyedBlockCount / initialBlockCount;
			if (progress >= 0.995f)
			{
				if (GameManager.Instance != null && !GameManager.Instance.finised)
				{
					GameManager.Instance.FinishLevel();
				}
			}
		}
	}

	private void UpdateUI()
	{
		float pct = GetProgressPct();
		if (_slider != null)
		{
			_slider.value = pct;
		}
		if (_textProgress != null)
		{
			_textProgress.text = Mathf.RoundToInt(pct * 100f) + "%";
		}
	}

	public void ResetCount()
	{
		currentCount = 0;
		DestroyedByPlayer = 0;
		DestroyedByOpponent = 0;
		_isUiDirty = true;
	}

	internal void KillBlockOpponent()
	{
		currentCount++;
		DestroyedByOpponent++;
		_isUiDirty = true;
	}
}
