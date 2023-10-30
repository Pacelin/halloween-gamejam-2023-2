using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SoulPhrasePresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _hidingTime;
    [SerializeField] private float _phraseCharacterTimeIn;

	public void HidePhrase(float delay = 0, Action callback = null) => StartCoroutine(Hiding(delay, callback));
    public void ShowPhrase(string phrase, float delay = 0, Action callback = null) => StartCoroutine(Showing(phrase, delay, callback));

    private IEnumerator Hiding(float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);

        for (float t = 0; t < _hidingTime; t+= Time.deltaTime)
        {
            _text.alpha = Mathf.Lerp(1, 0, t / _hidingTime);
            yield return null;
        }
        _text.text = "";

        if (callback != null)
            callback();
    }

    private IEnumerator Showing(string phrase, float delay, Action callback)
    {
		yield return new WaitForSeconds(delay);

		_text.alpha = 1;
		_text.text = "";
		foreach (var ch in phrase)
        {
            _text.text += ch;
            if (ch == ' ')
                continue;

            yield return new WaitForSeconds(_phraseCharacterTimeIn);
		}

		if (callback != null)
			callback();
    }
}
