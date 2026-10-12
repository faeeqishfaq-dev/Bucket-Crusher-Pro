#pragma warning disable CS0414

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Upgrade : MonoBehaviour
{
    private GameObject _currentStick;
    private GameObject _opponentStick;

    public List<StickUpgradeLevels> _customStickLevels;

    [SerializeField]
    private GameObject _railsStick;

    [Header("LENGTH PREFABS")]
    [Tooltip("Order must be: Element 0 = Base, Element 1 = +1 length step, Element 2 = +2, etc.")]
    [SerializeField]
    private GameObject[] _stickLevels;

    [SerializeField]
    private GameObject[] _opponentStickPrefabs;

    [Header("STICK SPAWN")]
    public Vector3 _stickPosition = new Vector3(0.58f, 0f, 0f);
    public Quaternion _stickRotation = Quaternion.identity;

    // Kept only so old Inspector serialization is not broken.
    // It is NOT used for physical length anymore.
    [SerializeField]
    public float _lengthIncrement = 0.15f;

    private int _lastPlayerLength = -1;
    private int _lastOpponentLength = -1;

    private void Start()
    {
        UpdateAllStage();
    }

    public void UpdateAllStage()
    {
        UpdateLong();
        UpdatePower();
        UpdateSize();
        UpdateOil();
    }

    // =========================================================
    // LENGTH
    // =========================================================

    public void UpdateLong()
    {
        if (_stickLevels == null || _stickLevels.Length == 0)
        {
            Debug.LogError("[Upgrade] _stickLevels is empty.");
            return;
        }

        int lengthLevel = PlayerPrefs.GetInt("LengthUpgrade", 0);

        // Never allow an invalid prefab index.
        int prefabIndex = Mathf.Clamp(
            lengthLevel,
            0,
            _stickLevels.Length - 1
        );

        GameObject requiredPrefab = _stickLevels[prefabIndex];

        if (requiredPrefab == null)
        {
            Debug.LogError(
                "[Upgrade] Missing stick prefab at _stickLevels[" +
                prefabIndex + "]"
            );
            return;
        }

        // Nothing changed.
        // IMPORTANT: do NOT rebuild/reposition physics every frame.
        if (_currentStick != null &&
            _lastPlayerLength == lengthLevel)
        {
            EnsureCurrentStickReferences();
            return;
        }

        ReplaceCurrentStick(requiredPrefab, lengthLevel);
    }

    private void ReplaceCurrentStick(
        GameObject prefab,
        int lengthLevel)
    {
        Transform parent = ResolveStickParent();

        if (parent == null)
        {
            Debug.LogError("[Upgrade] Could not find stick parent.");
            return;
        }

        GameObject oldStick = _currentStick;

        // Stop old physics BEFORE enabling the new chain.
        if (oldStick != null)
        {
            oldStick.SetActive(false);
        }

        // Spawn the prefab that already contains the correct
        // chain length / joints / anchors.
        GameObject newStick = Instantiate(prefab, parent);

        newStick.name = prefab.name + "_Runtime";

        newStick.SetActive(false);

        newStick.transform.localPosition = _stickPosition;
        newStick.transform.localRotation = _stickRotation;

        // Never inherit an accidental scale from previous upgrades.
        newStick.transform.localScale = Vector3.one;

        _currentStick = newStick;
        _lastPlayerLength = lengthLevel;

        newStick.SetActive(true);

        // Connect Saw/Head back to controls/camera.
        BindStickReferences(newStick);

        // Restore already purchased Size + Power on the new Head.
        UpdateSize();
        UpdatePower();

        // Adjust player movement compensation for the new prefab.
        Control ctrl = Object.FindAnyObjectByType<Control>();

        if (ctrl != null)
        {
            ctrl.UpdateSpeedForLength(newStick);
        }

        if (oldStick != null)
        {
            Destroy(oldStick);
        }

        // Rebind next frame as protection against any OnDisable /
        // OnDestroy logic belonging to the previous stick.
        StartCoroutine(RebindNextFrame(newStick));
    }

    private Transform ResolveStickParent()
    {
        LevelOrientation orientation =
            Object.FindAnyObjectByType<LevelOrientation>();

        if (orientation != null)
        {
            Transform orientationParent =
                orientation.GetCurrentStickParent();

            if (orientationParent != null)
            {
                return orientationParent;
            }
        }

        // Preserve current parent if a stick already exists.
        if (_currentStick != null &&
            _currentStick.transform.parent != null)
        {
            return _currentStick.transform.parent;
        }

        GameObject rightExcavator = GameObject.Find("Right");

        if (rightExcavator != null)
        {
            return rightExcavator.transform;
        }

        // Final fallback.
        return transform;
    }

    private void BindStickReferences(GameObject stick)
    {
        if (stick == null)
            return;

        Head head = stick.GetComponentInChildren<Head>(true);

        if (head == null)
        {
            Debug.LogError(
                "[Upgrade] Head not found inside: " + stick.name
            );
            return;
        }

        Rigidbody headRb = head.GetComponent<Rigidbody>();

        if (headRb == null)
        {
            headRb = head.GetComponentInChildren<Rigidbody>(true);
        }

        Control control = Object.FindAnyObjectByType<Control>();

        if (control != null && headRb != null)
        {
            control.SetHead(headRb);
        }

        Cam cam = Object.FindAnyObjectByType<Cam>();

        if (cam != null)
        {
            cam.SetHead(head.transform);
        }
    }

    private void EnsureCurrentStickReferences()
    {
        if (_currentStick == null)
            return;

        BindStickReferences(_currentStick);
    }

    private IEnumerator RebindNextFrame(GameObject expectedStick)
    {
        yield return null;

        if (_currentStick == null ||
            _currentStick != expectedStick)
        {
            yield break;
        }

        BindStickReferences(_currentStick);
    }

    // =========================================================
    // SIZE
    // =========================================================

    public void UpdateSize(bool max = false)
    {
        Head h = null;

        if (_currentStick != null)
        {
            h = _currentStick.GetComponentInChildren<Head>(true);
        }

        if (h == null)
        {
            h = Object.FindAnyObjectByType<Head>();
        }

        if (h == null)
            return;

        int sizeLevel =
            PlayerPrefs.GetInt("SizeUpgrade", 0);

        float scale =
            1f + sizeLevel * 0.1f;

        h.transform.localScale =
            Vector3.one * scale;
        h.UpdateCrushingPower();
    }

    // =========================================================
    // POWER
    // =========================================================

    private void SetupStickPower(
        GameObject stick,
        bool max = false)
    {
        Head h = null;

        if (stick != null)
        {
            h = stick.GetComponentInChildren<Head>(true);
        }

        if (h == null)
        {
            h = Object.FindAnyObjectByType<Head>();
        }

        if (h != null)
        {
            h.UpdateRotationSpeed();
            h.UpdateCrushingPower();
        }
    }

    public void UpdatePower(bool max = false)
    {
        SetupStickPower(_currentStick, max);
    }

    // =========================================================
    // OIL
    // =========================================================

    public void UpdateOil()
    {
        Oil oil = Object.FindAnyObjectByType<Oil>(FindObjectsInactive.Include);

        if (oil != null)
        {
            oil.UpdateValues();
        }
    }

    // =========================================================
    // EXISTING PUBLIC API
    // =========================================================

    public void MaxPower()
    {
        UpdatePower(true);
    }

    public void CurrentPower()
    {
        UpdatePower(false);
    }

    public void PlayerUpgraded()
    {
        UpdateAllStage();
    }

    public void OpponentUpgraded()
    {
    }

    public void UpdatePlayerLength()
    {
        UpdateLong();
    }

    public void RefreshOpponentStick()
    {
    }

    public void UpdateOpponentLength()
    {
    }

    public void UpdatePlayerSize(bool max = false)
    {
        UpdateSize(max);
    }

    public void UpdateOpponentSize(bool max = false)
    {
    }
}