using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Game
{
	public static event Action OnSoulAppear;
	public static event Action<SoulType, SoulReincornationType> OnSuccessedReincornation;
	public static event Action<SoulType, SoulReincornationType> OnFailedReincornation;
	public static event Action OnGameFinishedSuccessful;
	public static event Action OnGameFinishedFailed;

	public SoulModel CurrentSoul { get; private set; }
	public SoulPresenter CurrentSoulPresenter { get; private set; }
 
	[Inject] private SoulFactory _soulFactory;
	[Inject] private SoulPresenterFactory _soulPresenterFactory;
	[Inject] private SoulPhrasePresenter _phrasesPresenter;

	[Inject] private PotPresenter _pot;

	private Vector3 _soulsPosition;
	private int _minimumScoreForReincornation;
	private bool _currentIsDevil;

	public Game(Vector3 soulPresentersPosition, int minimumScoreForReincornation)
	{
		_soulsPosition = soulPresentersPosition;
		_minimumScoreForReincornation = minimumScoreForReincornation;
		_currentIsDevil = false;
	}

	public void CreateSoul()
	{
		if (_currentIsDevil) return;

		var soul = _soulFactory.Create(out _currentIsDevil);
		var presenter = _soulPresenterFactory.Create(soul);
		presenter.transform.position = _soulsPosition;
		presenter.Appear();
		OnSoulAppear?.Invoke();

		_phrasesPresenter.ShowPhrase(soul.Phrase, 0.5f);

		CurrentSoul = soul;
		CurrentSoulPresenter = presenter;
	}

	public IEnumerator StartReincarnation()
	{
		if (CurrentSoul == null) yield break;
		_pot.StartReincarnation();
		yield return new WaitForSeconds(0.5f);
		CurrentSoulPresenter.Disappear();
		var items = _pot.Items;
		_pot.ResetPot();

		if (items.Length == 0)
		{
			if (_currentIsDevil)
				yield return DevilEmptyRein();
			else
				yield return UnknownRein();
		}
		else if (_currentIsDevil)
		{
			yield return DevilNotEmptyRein();
		}
		else
		{
			var soul = CalculateSoulType(items);
			var reincornation = CalculateSoulReincornationType(items);

			yield return new WaitForSeconds(0.5f);
			_phrasesPresenter.HidePhrase();

			if (CurrentSoul.SoulTypesResult.Contains(soul) &&
				CurrentSoul.ReincornationTypesResult.Contains(reincornation))
			{
				OnSuccessedReincornation?.Invoke(soul, reincornation);
				yield return GoodRein(soul, reincornation);
			}
			else
			{
				OnFailedReincornation?.Invoke(soul, reincornation);
				if (soul == SoulType.Unknown || reincornation == SoulReincornationType.Unknown)
					yield return UnknownRein();
				else
					yield return BadRein(soul, reincornation);
			}
		}

		yield return new WaitForSeconds(2f);
		CurrentSoul = null;
		CurrentSoulPresenter = null;
		CreateSoul();
	}

	private IEnumerator DevilEmptyRein()
	{
		yield return new WaitForSeconds(1.5f);
		_phrasesPresenter.ShowPhrase("Отличная работа... Да... Это был сатана, он иногда приходит... Главное... Чтобы ты всегда выполнял его желания...");
		yield return new WaitForSeconds(7f);
		OnGameFinishedSuccessful?.Invoke();
	}
	private IEnumerator DevilNotEmptyRein()
	{
		yield return new WaitForSeconds(1.5f);
		OnGameFinishedFailed?.Invoke();
	}
	private IEnumerator UnknownRein()
	{
		yield return new WaitForSeconds(1.5f);
		_phrasesPresenter.ShowPhrase("Твой выбор привел к тому, что душа раскололась на части, пытаясь воплотиться во множестве существ...\n" +
			"Тщательнее подбирай ингредиенты...");
		yield return new WaitForSeconds(6f);
		_phrasesPresenter.HidePhrase();
	}
	private IEnumerator BadRein(SoulType soul, SoulReincornationType reincornation)
	{
		yield return new WaitForSeconds(1.5f);
		_phrasesPresenter.ShowPhrase("Мда... Такой выбор ингредиентов мы не одобряем... Душа перевоплотилась в теле " +
			SoulTypePresenter.SoulTypes[soul] + " и стала " +
			SoulTypePresenter.SoulReincornationTypes[reincornation] + ".\n" +
			"Теперь душа вынуждена жить в мире, где ей нет места...");
		yield return new WaitForSeconds(7f);
		_phrasesPresenter.HidePhrase();
		yield return new WaitForSeconds(1.5f);
		_phrasesPresenter.ShowPhrase("Что ж... Перейдем к следующей душе...");
		yield return new WaitForSeconds(4f);
		_phrasesPresenter.HidePhrase();
	}
	private IEnumerator GoodRein(SoulType soul, SoulReincornationType reincornation)
	{
		yield return new WaitForSeconds(1.5f);
		_phrasesPresenter.ShowPhrase("Отличная работа, душа перевоплотилась в теле " +
			SoulTypePresenter.SoulTypes[soul] + " и стала " +
			SoulTypePresenter.SoulReincornationTypes[reincornation] + ".\n" +
			"Душа благодарна тебе.");
		yield return new WaitForSeconds(7f);
		_phrasesPresenter.HidePhrase();
		yield return new WaitForSeconds(1f);
		_phrasesPresenter.ShowPhrase("Перейдем к следующей душе.");
		yield return new WaitForSeconds(4f);
		_phrasesPresenter.HidePhrase();
	}
	private SoulType CalculateSoulType(ItemData[] items)
	{
		var dict = new Dictionary<SoulType, int>();
		var scores = items.Select(item => item.SoulScores);
		foreach (var score in scores)
		{
			foreach (var scoreItem in score)
			{
				if (dict.ContainsKey(scoreItem.SoulType))
					dict[scoreItem.SoulType] += scoreItem.Score;
				else
					dict.Add(scoreItem.SoulType, scoreItem.Score);
			}
		}
		if (dict.Count == 0)
			return SoulType.Unknown;
		var maxValue = dict.Max(pair => pair.Value);
		if (maxValue < _minimumScoreForReincornation)
			return SoulType.Unknown;
		var maxPair = dict.First(pair => pair.Value == maxValue);

		return maxPair.Key;
	}
	private SoulReincornationType CalculateSoulReincornationType(ItemData[] items)
	{
		var dict = new Dictionary<SoulReincornationType, int>();
		var scores = items.Select(item => item.ReincarnationScores);
		foreach (var score in scores)
		{
			foreach (var scoreItem in score)
			{
				if (dict.ContainsKey(scoreItem.SoulType))
					dict[scoreItem.SoulType] += scoreItem.Score;
				else
					dict.Add(scoreItem.SoulType, scoreItem.Score);
			}
		}
		if (dict.Count == 0)
			return SoulReincornationType.Unknown;
		var maxValue = dict.Max(pair => pair.Value);
		if (maxValue < _minimumScoreForReincornation)
			return SoulReincornationType.Unknown;
		var maxPair = dict.First(pair => pair.Value == maxValue);

		return maxPair.Key;
	}
}