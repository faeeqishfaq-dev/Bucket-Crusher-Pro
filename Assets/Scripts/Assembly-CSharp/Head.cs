using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Head : MonoBehaviour
{
	[SerializeField]
	private Rigidbody[] _rigs;

	private Progress progress;

	public bool isOpponent;

	private GameplayAudio gameplayAudio;

	private GameplayHaptic gameplayHaptic;

	private float rotationSpeed = 360f;

	public int amount = 1;

	private MapCreatorEditor _mapGenerator;

	private int _addedDifficulty;

	private int _randomRoll;

	private List<Collider> _colliders;

	private Transform[] _visualSaws;

	private void Start()
    {
        if (GetComponent<SawDebrisPusher>() == null) gameObject.AddComponent<SawDebrisPusher>();
		progress = Progress.Instance;
		gameplayAudio = Object.FindAnyObjectByType<GameplayAudio>();
		gameplayHaptic = Object.FindAnyObjectByType<GameplayHaptic>();
		_mapGenerator = Object.FindAnyObjectByType<MapCreatorEditor>();

		if (!isOpponent)
		{
			Rigidbody rb = GetComponent<Rigidbody>();
			if (rb == null) rb = GetComponentInChildren<Rigidbody>();
			Control ctrl = Object.FindAnyObjectByType<Control>();
			if (ctrl != null && rb != null)
			{
				ctrl.SetHead(rb);
				// Find the stick root (topmost parent that is not the excavator itself)
				// and update the speed multiplier for the current arm length.
				Transform stickRoot = transform;
				while (stickRoot.parent != null && stickRoot.parent.GetComponent<Control>() == null)
				{
					stickRoot = stickRoot.parent;
				}
				ctrl.UpdateSpeedForLength(stickRoot.gameObject);
			}
			Cam cam = Object.FindAnyObjectByType<Cam>();
			if (cam != null)
			{
				cam.SetHead(transform);
			}
		}

		List<Transform> visualSawsList = new List<Transform>();
		foreach (Transform child in transform)
		{
			if (child.name.StartsWith("saw_default"))
			{
				visualSawsList.Add(child);
				Animator anim = child.GetComponent<Animator>();
				if (anim != null) anim.enabled = false;
			}
		}
		_visualSaws = visualSawsList.ToArray();

		UpdateRotationSpeed();
	}

	public static void AddPhysics(Collider other)
	{
		if (other == null) return;
		other.enabled = false;
		
		if (VoxelWallConverter.Instance != null)
		{
			VoxelWallConverter.Instance.OnBoneCrushed(other.transform);
		}
		
		other.transform.localScale = Vector3.zero;
	}

	private void OnTriggerEnter(Collider other)
	{
		Trigged(other);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision != null && collision.collider != null)
		{
			Trigged(collision.collider);
		}
	}

	private Collider[] _hitResults = new Collider[256];
	private SphereCollider _col;

	void FixedUpdate()
	{
		if (_col == null) _col = GetComponent<SphereCollider>();
		if (_col != null)
		{
			float r = _col.radius * Mathf.Max(transform.lossyScale.x, Mathf.Max(transform.lossyScale.y, transform.lossyScale.z));
			int hits = Physics.OverlapSphereNonAlloc(transform.TransformPoint(_col.center), r, _hitResults);
			for (int i = 0; i < hits; i++)
			{
				Collider hit = _hitResults[i];
				if (hit != null && hit != _col && hit.enabled)
				{
					if (hit.transform.root != transform.root)
					{
						Trigged(hit);
					}
				}
			}
		}
	}

	private float currentCrushingPower = -1f;

	public void UpdateCrushingPower()
	{
		int sizePurchases = PlayerPrefs.GetInt("SizeUpgrade", 0);
		int powerPurchases = PlayerPrefs.GetInt("PowerUpgrade", 0);
		
		float power = 0.10f + (sizePurchases + powerPurchases) * 0.05f;
		currentCrushingPower = Mathf.Clamp(power, 0.10f, 1.0f);
	}

	private float GetCrushingPower()
	{
		if (currentCrushingPower < 0f)
		{
			UpdateCrushingPower();
		}
		return currentCrushingPower;
	}

	void Trigged(Collider other)
	{
		if (other == null || !other.enabled) return;

		// Check if game is in a valid playing state (Oil spending is active)
		if (GameManager.Instance != null && GameManager.Instance.currentOilUI != null)
		{
			if (!GameManager.Instance.currentOilUI.IsSpendingOil) return;
		}

		// Optimize: Use layer 10 which is exclusively for uncrushed wall bones
		if (other.gameObject.layer == 10)
		{
			// CRUSHING POWER PROGRESSION
			if (UnityEngine.Random.value > GetCrushingPower())
			{
				return; // Fail to crush this frame, simulates weaker/slower grinding
			}

			AddPhysics(other);

			if (VoxelWallConverter.Instance != null)
			{
				VoxelWallConverter.Instance.OnBoneCrushed(other.transform);
			}

			if (progress != null)
			{
				progress.KillBlock();
			}
			else if (Progress.Instance != null)
			{
				Progress.Instance.KillBlock();
			}

			if (gameplayAudio != null)
			{
				gameplayAudio.PlaySound(GameplaySoundType.DigSound);
			}
			if (gameplayHaptic != null)
			{
				gameplayHaptic.PlayHaptic(HapticType.Dig);
			}
		}
	}

	public void SetKinematic(bool active)
	{
		if (_rigs != null)
		{
			for (int i = 0; i < _rigs.Length; i++)
			{
				if (_rigs[i] != null) _rigs[i].isKinematic = active;
			}
		}
	}

	public void UpdateRotationSpeed()
	{
		int powerLevel = PlayerPrefs.GetInt("PowerUpgrade", 0);
		rotationSpeed = 360f + powerLevel * 60f;
	}

	private void Update()
	{
		if (_rigs != null && _rigs.Length > 0)
		{
			for (int i = 0; i < _rigs.Length; i++)
			{
				if (_rigs[i] != null)
				{
					// FIX: Do not manually rotate the chain segments (p1, p2, p3) or the Head itself.
					// Rotating them via Transform while they use Rigidbody/ConfigurableJoints causes massive 
					// physics conflicts, leading to stiff, delayed, and unstable movement.
					string rbName = _rigs[i].gameObject.name;
					if (rbName == "Head" || rbName.StartsWith("p") || rbName == "base")
					{
						continue;
					}

					_rigs[i].transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
				}
			}
		}

		if (_visualSaws != null)
		{
			for (int i = 0; i < _visualSaws.Length; i++)
			{
				if (_visualSaws[i] != null)
				{
					// saw_default_01 has a local rotation offset (-90 on X), so its local Z axis 
					// is not the correct spin axis. We must rotate it around the Head's Z axis.
					_visualSaws[i].RotateAround(transform.position, transform.forward, rotationSpeed * Time.deltaTime);
				}
			}
		}
	}
}
