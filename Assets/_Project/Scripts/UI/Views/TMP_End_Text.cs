using TMPro;
using UnityEngine;

public class TMP_End_Text : MonoBehaviour
{
	[SerializeField] private TMP_Text _goodReinsResults;
	[SerializeField] private string _goodReinsTemplate;
	[SerializeField] private TMP_Text _badReinsResults;
	[SerializeField] private string _badReinsTemplate;
	[SerializeField] private TMP_Text _aboutDevil;
	[Multiline]
	[SerializeField] private string _textIfDevilHappy;
	[Multiline]
	[SerializeField] private string _textIfDevilNotHappy;

	private void Start()
	{
		_goodReinsResults.text = string.Format(_goodReinsTemplate, GameFinal.SuccessfulReincornationsCount);
		_badReinsResults.text = string.Format(_badReinsTemplate, GameFinal.FailedReincornationsCount);
		_aboutDevil.text = GameFinal.DevilIsHappy ? _textIfDevilHappy : _textIfDevilNotHappy;
	}
}
