using UnityEngine;

public class MultipleSawsUpgrade : MonoBehaviour
{
	[SerializeField]
	private Transform[] _heads;

	[SerializeField]
	private CapsuleCollider _mainCollider;

	[SerializeField]
	private GameObject _2x;

	[SerializeField]
	private GameObject _3x;

	public Item SawsItem;

	public void UpdatePoower(float radius)
	{
	}

	public void UpdateAnimationSpeed(float speed)
	{
	}

	private void Start()
	{
	}

	private void OnItemPurchase()
	{
	}

	private void OnDestroy()
	{
	}
}
