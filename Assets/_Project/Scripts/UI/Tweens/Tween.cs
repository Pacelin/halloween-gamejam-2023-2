using System;
using System.Collections;
using UnityEngine;

public abstract class Tween : MonoBehaviour
{
	[SerializeField] private bool _playOnAwake;
	[SerializeField] private float _playOnAwakeDelay;

	private void Start()
	{
		if (_playOnAwake) Play(_playOnAwakeDelay);
	}

	public void Play(float delay, Action callback = null) =>
		StartCoroutine(Playing(delay, callback));

	protected abstract IEnumerator Play();

	private IEnumerator Playing(float delay, Action callback)
	{
		yield return new WaitForSeconds(delay);
		yield return Play();

		if (callback != null)
			callback();
	}
}