using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitions : MonoBehaviour
{
	public static SceneTransitions Instance => _instance;
	private static SceneTransitions _instance;

	[SerializeField] private CanvasGroup _fadeCanvasGroup;
	[SerializeField] private Image _mainImage;
	[Space]
	[SerializeField] private float _fadeInDuration;
	[SerializeField] private float _fadeDuration;
	[SerializeField] private float _fadeOutDuration;

	public void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			_fadeCanvasGroup.alpha = 0;
			_fadeCanvasGroup.blocksRaycasts = false;
			_fadeCanvasGroup.interactable = false;
			return;
		}
		Destroy(gameObject);
	}

	public void SetMainImageColor(Color color)
	{
		_mainImage.color = color;
	}

	public void StartTransition(float delay, int buildIndex) => StartCoroutine(Transition(delay, buildIndex));
	private IEnumerator Transition(float delay, int buildIndex)
	{
		yield return new WaitForSeconds(delay);
		_fadeCanvasGroup.blocksRaycasts = true;
		for (float t = 0; t < _fadeInDuration; t += Time.deltaTime)
		{
			_fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, t / _fadeInDuration);
			yield return null;
		}
		_fadeCanvasGroup.alpha = 1;

		var time = Time.time;
		var task = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
		yield return new WaitUntil(() => task.isDone);
		time = Time.time - time;

		yield return new WaitForSeconds(_fadeDuration - time);

		for (float t = 0; t < _fadeOutDuration; t += Time.deltaTime)
		{
			_fadeCanvasGroup.alpha = Mathf.Lerp(1, 0, t / _fadeOutDuration);
			yield return null;
		}
		_fadeCanvasGroup.alpha = 0;
		_fadeCanvasGroup.blocksRaycasts = false;
	}
}