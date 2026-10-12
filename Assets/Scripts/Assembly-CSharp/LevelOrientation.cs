using UnityEngine;

public class LevelOrientation : MonoBehaviour
{
	private LevelPosType levelPosType;

	[SerializeField]
	private Transform OilTank;

	[SerializeField]
	private GameObject _rightExcavator;

	[SerializeField]
	private GameObject _leftExcavator;

	[SerializeField]
	private GameObject _archExcavator;

	[SerializeField]
	private GameObject _toptExcavator;

	[SerializeField]
	private GameObject _railExcavator;

	[Space]
	[SerializeField]
	private Transform _rightOilTankTransform;

	[SerializeField]
	private Transform _leftOilTankTransform;

	[SerializeField]
	private Transform _archOilTankTransform;

	[SerializeField]
	private Transform _topOilTankTransform;

	[Space]
	[SerializeField]
	private Transform _rightStickTransform;

	[SerializeField]
	private Transform _leftStickTransform;

	[SerializeField]
	private Transform _archStickTransform;

	[SerializeField]
	private Transform _topStickTransform;

	[SerializeField]
	private Transform _railStickTransform;

	[Space]
	[SerializeField]
	private Transform _map;

	[SerializeField]
	private Transform _leftBoard;

	[SerializeField]
	private Transform _rightBoard;

	[SerializeField]
	private Transform _left2Board;

	[SerializeField]
	private Transform _right2Board;

	[Space]
	[SerializeField]
	private Transform _leftTreadmill;

	[SerializeField]
	private Transform _rightTreadmill;

	[Space]
	[SerializeField]
	private Transform _levelObject;

	[SerializeField]
	private GameObject _blueCubeObject;

	private void Start()
	{
		DetermineLevelPosType(levelPosType);
	}

	public void DetermineLevelPosType(LevelPosType posType = LevelPosType.Right)
	{
		levelPosType = posType;

		if (_rightExcavator != null) _rightExcavator.SetActive(posType == LevelPosType.Right);
		if (_leftExcavator != null) _leftExcavator.SetActive(posType == LevelPosType.Left);
		if (_archExcavator != null) _archExcavator.SetActive(posType == LevelPosType.Arch);
		if (_toptExcavator != null) _toptExcavator.SetActive(posType == LevelPosType.Top);
		if (_railExcavator != null) _railExcavator.SetActive(posType == LevelPosType.Rail);

		if (OilTank != null)
		{
			Transform targetTank = null;
			switch (posType)
			{
				case LevelPosType.Left: targetTank = _leftOilTankTransform; break;
				case LevelPosType.Arch: targetTank = _archOilTankTransform; break;
				case LevelPosType.Top: targetTank = _topOilTankTransform; break;
				default: targetTank = _rightOilTankTransform; break;
			}
			if (targetTank != null)
			{
				OilTank.position = targetTank.position;
				OilTank.rotation = targetTank.rotation;
			}
		}
	}

	public Transform GetCurrentStickParent()
	{
		switch (levelPosType)
		{
			case LevelPosType.Left:
				return _leftStickTransform != null ? _leftStickTransform : transform;
			case LevelPosType.Arch:
				return _archStickTransform != null ? _archStickTransform : transform;
			case LevelPosType.Top:
				return _topStickTransform != null ? _topStickTransform : transform;
			case LevelPosType.Rail:
				return _railStickTransform != null ? _railStickTransform : transform;
			default:
				return _rightStickTransform != null ? _rightStickTransform : transform;
		}
	}
}
