using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Voodoo.Meta
{
	public class LeaderboardPlayerSlot : MonoBehaviour
	{
		[SerializeField]
		private TMP_Text _rankTxt;

		[SerializeField]
		private TMP_Text _playerName;

		[SerializeField]
		private TMP_Text _score;

		[SerializeField]
		private Image _background;

		[SerializeField]
		private Image _rankBackground;

		[SerializeField]
		private GameObject _outline;

		[SerializeField]
		private Image _medal;

		[SerializeField]
		private List<GameObject> _fxValidation;

		[Header("Optional")]
		[Tooltip("Remove if you don't want the player's name to be editable, remember to deactivate the GameObject too.")]
		[SerializeField]
		private TMP_InputField _playerNameInputField;

		[Tooltip("Remove if you don't want the player's flag to be visible, remember to deactivate the GameObject too.")]
		[SerializeField]
		private Image _flagIcon;

		private int _rank;

		private RectTransform _rectTransform;

		private LeaderboardSlotInfo _slotInfo;

		public float Height => 0f;

		public Vector2 AnchoredPosition
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public int Rank
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public RectTransform RectTransform => null;

		public event Action<string> nameChanged
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

		public void Initialize(LeaderboardSlotInfo slotInfo)
		{
		}

		public void Refresh(LeaderboardSlotInfo slotInfo)
		{
		}

		public void Refresh(int rank, LeaderboardBackgroundColors backgroundColors, Sprite medal)
		{
		}

		public void MoveSlot(float margin, int direction)
		{
		}

		private void SetSlotName(string slotName, bool isPlayer)
		{
		}

		public void UpdateScore(int score)
		{
		}

		public void OnValidation()
		{
		}

		public void OnPlayerNameChanged(string newName)
		{
		}

		private void SetFlag(Sprite flag)
		{
		}

		private void SetMedal(Sprite medal)
		{
		}
	}
}
