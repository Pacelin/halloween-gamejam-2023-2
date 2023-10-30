using System;
using System.Collections;
using UnityEngine;

public class Animation_Tween : Tween
{
	public event Action<int> OnAnimationEvent;
    [SerializeField] private Animation _target;

	protected override IEnumerator Play()
	{
		_target.Play();
		yield return new WaitWhile(() => _target.isPlaying);
	}

	private void PoolEvent(int eventId) =>
		OnAnimationEvent?.Invoke(eventId);
}
