using System.Collections;
using TMPro;
using UnityEngine;

public class TMP_Text_ThicknessTween : Tween
{
    [SerializeField] private TMP_Text _target;
	[Space]
    [SerializeField] private float _fromThickness;
    [SerializeField] private float _toThickness;
	[SerializeField] private float _duration;
	[SerializeField] private float _pow = 1;

	private Material _textMaterial;

	private const string THICKNESS_OPTION = "_OutlineWidth";

	private void Awake() =>
		_textMaterial = _target.fontMaterial;

	protected override IEnumerator Play()
	{
		for (float t = 0; t < _duration; t += Time.deltaTime)
		{
			_textMaterial.SetFloat(THICKNESS_OPTION, Mathf.Lerp(_fromThickness, _toThickness, Mathf.Pow(t / _duration, _pow)));
			yield return null;
		}
		_textMaterial.SetFloat(THICKNESS_OPTION, _toThickness);
	}
}
