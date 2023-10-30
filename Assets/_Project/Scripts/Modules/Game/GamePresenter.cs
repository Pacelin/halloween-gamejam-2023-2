using System.Collections;
using UnityEngine;
using Zenject;

public class GamePresenter : MonoBehaviour
{
	[Multiline]
	[SerializeField] private string[] _firstPhrases;
	[SerializeField] private AudioClip _soulAppearSound;
	[SerializeField] private float _soulAppearSoundVolume;

	[Inject] private AudioSource _mainSource;
	[Inject] private SoulPhrasePresenter _phrasesPresenter;
	[Inject] private Game _game;

	private void OnEnable() =>
		Game.OnSoulAppear += Game_OnSoulAppear;
	private void OnDisable() =>
		Game.OnSoulAppear -= Game_OnSoulAppear;

	private void Game_OnSoulAppear() =>
		_mainSource.PlayOneShot(_soulAppearSound, _soulAppearSoundVolume);

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.5f);

		foreach (var phrase in _firstPhrases)
		{
			bool showed = false;
			_phrasesPresenter.ShowPhrase(phrase, 0, () => showed = true);
			
			yield return new WaitUntil(() => showed);
			
			_phrasesPresenter.HidePhrase(1.5f);
			yield return new WaitForSeconds(2.5f);
		}

		_game.CreateSoul();
	}

	public void StartReincarnation() =>
		StartCoroutine(_game.StartReincarnation());
}