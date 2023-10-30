using System.Collections;
using TMPro;
using UnityEngine;

public class TMP_Text_DilateTween : Tween
{
    [SerializeField] private TMP_Text _target;
	[Space]
    [SerializeField] private float _fromDilate;
    [SerializeField] private float _toDilate;
	[SerializeField] private float _duration;
	[SerializeField] private float _pow = 1;

	private Material _textMaterial;

	private const string FACE_DILATE_OPTION = "_FaceDilate";

	private void Awake() =>
		_textMaterial = _target.fontMaterial;

	protected override IEnumerator Play()
	{
		for (float t = 0; t < _duration; t += Time.deltaTime)
		{
			_textMaterial.SetFloat(FACE_DILATE_OPTION, Mathf.Lerp(_fromDilate, _toDilate, Mathf.Pow(t / _duration, _pow)));
			yield return null;
		}
		_textMaterial.SetFloat(FACE_DILATE_OPTION, _toDilate);
	}
}
