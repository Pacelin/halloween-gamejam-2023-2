using System.Collections;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    [SerializeField] private ItemPresenter _itemPrefab;

	private ItemPresenter _currentPresenter;

	private void Awake()
	{
		_currentPresenter =  Instantiate(_itemPrefab, transform.position, transform.rotation);
		_currentPresenter.SetTrigger();
		_currentPresenter.OnGrab += CurrentPresenter_OnGrab;
	}

	private void OnDisable()
	{
		if (_currentPresenter != null)
			_currentPresenter.OnGrab -= CurrentPresenter_OnGrab;
	}

	private void CurrentPresenter_OnGrab()
	{
		_currentPresenter.OnGrab -= CurrentPresenter_OnGrab;
		_currentPresenter = null;
		StartCoroutine(CreateNewItem());
	}

	private IEnumerator CreateNewItem()
	{
		yield return new WaitForSeconds(1);
		_currentPresenter = Instantiate(_itemPrefab, transform.position, transform.rotation);
		var fadePresenter = _currentPresenter;
		_currentPresenter.SetTrigger();
		_currentPresenter.OnGrab += CurrentPresenter_OnGrab;

		var fromColor = Color.clear;
		var toColor = fadePresenter.SpriteRenderer.color;
		const float fadeInTime = 0.5f;
		for (float t = 0; t < fadeInTime; t += Time.deltaTime)
		{
			fadePresenter.SpriteRenderer.color = Color.Lerp(fromColor, toColor, t / fadeInTime);
			yield return null;
		}
		fadePresenter.SpriteRenderer.color = toColor;
	}
}