using UnityEngine;
using Voodoo.Pattern;

public class BonusFX : SingletonMB<BonusFX>
{
	[SerializeField]
	private ParticleSystem _FX;

	private void Start()
	{
	}

	public void MoveAndPlay(Vector3 worldPosition)
	{
	}
}
