using System.Collections;
using UnityEngine;

public class CanvasGroup_AlphaTween : Tween
{
	[SerializeField] private CanvasGroup _target;
	[Space]
	[SerializeField] private float _fromAlpha;
	[SerializeField] private float _toAlpha = 1;
	[SerializeField] private float _duration;
	[SerializeField] private float _pow;

	protected override IEnumerator Play()
	{
		_target.blocksRaycasts = false;
		_target.interactable = false;

		for (float t = 0; t < _duration; t += Time.deltaTime)
		{
			_target.alpha = Mathf.Lerp(_fromAlpha, _toAlpha, Mathf.Pow(t / _duration, _pow));
			yield return null;
		}
		_target.alpha = _toAlpha;
		
		_target.blocksRaycasts = true;
		_target.interactable = true;
	}
}
