using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Image_AlphaTween : Tween
{
	[SerializeField] private Image _target;
	[Space]
	[SerializeField] private float _fromAlpha;
	[SerializeField] private float _toAlpha = 1;
	[SerializeField] private float _duration;
	[SerializeField] private float _pow;

	protected override IEnumerator Play()
	{
		for (float t = 0; t < _duration; t += Time.deltaTime)
		{
			var a = Mathf.Lerp(_fromAlpha, _toAlpha, Mathf.Pow(t / _duration, _pow));
			_target.color = new Color(_target.color.r, _target.color.g, _target.color.b, a);
			yield return null;
		}
		_target.color = new Color(_target.color.r, _target.color.g, _target.color.b, _toAlpha);
	}
}
