using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MovingFx : MonoBehaviour
{
	public RectTransform m_RectTransform;

	private Sequence m_Sequence;

	public void Emit(Vector2 _TargetAnchorDestination, Action _OnCompleted = null)
	{
	}
}
