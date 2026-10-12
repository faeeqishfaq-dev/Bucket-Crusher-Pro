using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class Sell : MonoBehaviour
{

	[CompilerGenerated]
	private sealed class _003CKillCoin_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Rigidbody2D coin;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CKillCoin_003Ed__45(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CScaleAnim_003Ed__43 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Sell _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CScaleAnim_003Ed__43(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CWaitAndActivateRVButton_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Sell _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CWaitAndActivateRVButton_003Ed__38(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

    [Header("Multiplier Button Runtime Fix")]
    [SerializeField] private Canvas _multiplierCanvas;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Camera _uiEventCamera;

    public bool IsPlayer;

	[SerializeField]
	private Rigidbody2D _quater;

	[SerializeField]
	private Rigidbody2D _half;

	[SerializeField]
	private Rigidbody2D _one;

	[SerializeField]
	private Rigidbody2D _three;

	[SerializeField]
	private Rigidbody2D _five;

	[SerializeField]
	private Rigidbody2D _twenty;

	[SerializeField]
	private Transform _spawnPoint;

	[SerializeField]
	private GameObject _XText;

	[SerializeField]
	private GameObject _MultiplierObject;

	[SerializeField]
	private GameObject _MultiplierAnim;

	[SerializeField]
	private TextMeshPro _MultiplierText;

	[Header("Multiplier RV Button")]
	[SerializeField]
	private GameObject _MultiplierRV;

	[SerializeField]
	private GameObject _VideoImage;

	[SerializeField]
	private TextMeshProUGUI _MultiplierRVText;

	[SerializeField]
	private GameObject _MultiplierFX;

	private float[] values;

	private List<Rigidbody2D> _quaterL;

	private List<Rigidbody2D> _halfL;

	private List<Rigidbody2D> _oneL;

	private List<Rigidbody2D> _threeL;

	private List<Rigidbody2D> _fiveL;

	private List<Rigidbody2D> _twentyL;

	public TransitionMachine _transitionMachine;

	private Money Money;

	public float deltaMoney;

	private float X;

	public Item MultiplierUpgrade;

	private int RVmultiplier;

	private GameObject CoinParent;

	private float chosenMultiplier;

	private float lastAnim;

	private bool FirstTimeRVBoost
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

    private void Start()
    {
        SetupMultiplierButton();
    }

    private void OnGameEnd()
	{
	}

	private void OnDestroy()
	{
	}

	public void OnGameStart()
	{
	}

	[IteratorStateMachine(typeof(_003CWaitAndActivateRVButton_003Ed__38))]
	private IEnumerator WaitAndActivateRVButton()
	{
		return null;
	}

	public void AugmentMultiplier(int augment)
	{
	}

    public void OnClickRV()
    {
        // -------------------------------------------------
        // EXISTING REWARD AMOUNT
        // -------------------------------------------------
        CheckCpAd.instance.CallAd();
        float reward = 10000f;

        if (_MultiplierRVText != null &&
            !string.IsNullOrEmpty(_MultiplierRVText.text))
        {
            string text =
                _MultiplierRVText.text
                    .Replace("+", "")
                    .Replace("$", "")
                    .Replace(" ", "")
                    .Replace(",", "");

            float parsedReward;

            if (float.TryParse(text, out parsedReward))
            {
                reward = parsedReward;
            }
        }

        // -------------------------------------------------
        // GIVE REWARD DIRECTLY - NO AD
        // -------------------------------------------------

        GameProperties gp =
            UnityEngine.Object.FindAnyObjectByType<GameProperties>();

        if (gp != null)
        {
            gp.SetMoney(reward);
        }

        // -------------------------------------------------
        // REFRESH MONEY UI
        // -------------------------------------------------

        if (Money == null)
        {
            Money =
                UnityEngine.Object.FindAnyObjectByType<Money>();
        }

        if (Money != null)
        {
            Money.UpdateValue();
        }

        // -------------------------------------------------
        // EXISTING COIN BLAST
        // -------------------------------------------------

        if (_MultiplierFX != null)
        {
            if (!_MultiplierFX.activeSelf)
            {
                _MultiplierFX.SetActive(true);
            }

            ParticleSystem[] particles =
                _MultiplierFX.GetComponentsInChildren<ParticleSystem>(true);

            foreach (ParticleSystem particle in particles)
            {
                particle.Stop(
                    true,
                    ParticleSystemStopBehavior.StopEmittingAndClear
                );

                particle.Clear(true);
                particle.Play(true);
            }
        }

        // IMPORTANT:
        // Do NOT disable _MultiplierRV here.
        // Button must remain available for the next click.
    }

    private void OnTriggerEnter(Collider other)
	{
	}

	[IteratorStateMachine(typeof(_003CScaleAnim_003Ed__43))]
	private IEnumerator ScaleAnim()
	{
		return null;
	}

	private void EarnCoin(int value)
	{
	}

	[IteratorStateMachine(typeof(_003CKillCoin_003Ed__45))]
	private IEnumerator KillCoin(Rigidbody2D coin)
	{
		return null;
	}

	public void SetMultyplyer(float value)
	{
	}

	public void ResetMultyplyer()
	{
	}
    private void SetupMultiplierButton()
    {
        // -------------------------------------------------
        // WORLD SPACE CANVAS
        // -------------------------------------------------

        if (_multiplierCanvas == null)
        {
            _multiplierCanvas = GetComponent<Canvas>();
        }

        if (_multiplierCanvas != null)
        {
            if (_multiplierCanvas.renderMode == RenderMode.WorldSpace)
            {
                if (_uiEventCamera == null)
                {
                    _uiEventCamera = Camera.main;
                }

                if (_uiEventCamera == null)
                {
                    _uiEventCamera =
                        UnityEngine.Object.FindAnyObjectByType<Camera>();
                }

                _multiplierCanvas.worldCamera = _uiEventCamera;
            }

            // Keep this reward UI above gameplay touch UI.
            _multiplierCanvas.overrideSorting = true;

            if (_multiplierCanvas.sortingOrder < 100)
            {
                _multiplierCanvas.sortingOrder = 100;
            }
        }

        // -------------------------------------------------
        // GRAPHIC RAYCASTER
        // -------------------------------------------------

        GraphicRaycaster raycaster =
            GetComponent<GraphicRaycaster>();

        if (raycaster == null)
        {
            raycaster =
                gameObject.AddComponent<GraphicRaycaster>();
        }

        raycaster.enabled = true;

        // -------------------------------------------------
        // CANVAS GROUP
        // -------------------------------------------------

        CanvasGroup canvasGroup =
            GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        // -------------------------------------------------
        // FIND BUTTON
        // -------------------------------------------------

        if (_rewardButton == null)
        {
            if (_MultiplierRV != null)
            {
                _rewardButton =
                    _MultiplierRV.GetComponent<Button>();

                if (_rewardButton == null)
                {
                    _rewardButton =
                        _MultiplierRV.GetComponentInChildren<Button>(true);
                }
            }
        }

        if (_rewardButton == null)
        {
            _rewardButton =
                GetComponentInChildren<Button>(true);
        }

        if (_rewardButton != null)
        {
            _rewardButton.interactable = true;

            if (_rewardButton.targetGraphic != null)
            {
                _rewardButton.targetGraphic.raycastTarget = true;
            }

            // Prevent duplicate listeners.
            _rewardButton.onClick.RemoveListener(OnClickRV);
            _rewardButton.onClick.AddListener(OnClickRV);

            if (_MultiplierRV == null)
            {
                _MultiplierRV = _rewardButton.gameObject;
            }
        }

        // -------------------------------------------------
        // FIND EXISTING COIN BLAST
        // -------------------------------------------------

        if (_MultiplierFX == null && transform.parent != null)
        {
            Transform coinBlast =
                transform.parent.Find("CoinBlast");

            if (coinBlast != null)
            {
                _MultiplierFX = coinBlast.gameObject;
            }
        }
    }
}
