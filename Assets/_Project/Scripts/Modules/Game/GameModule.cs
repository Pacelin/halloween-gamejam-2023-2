using UnityEngine;
using Zenject;

public class GameModule : MonoInstaller
{
	[SerializeField] private PotPresenter _pot;
	[SerializeField] private PressPresenter _press;
	[SerializeField] private ItemGrabber _grabber;
	[Space]
	[SerializeField] private Transform _soulsPosition;
	[Space]
	[SerializeField] private SoulPhrasePresenter _phrasePresenter;
	[SerializeField] private SoulPresenter _presenterPrefab;
	[SerializeField] private SoulData[] _datas;
	[Header("Game Settings")]
	[SerializeField] private int _minimumScoreForReincornation;
	[SerializeField] private int _soulsCount;
	[SerializeField] private SoulData _devilSoul;

	public override void InstallBindings()
	{
		Container.Bind<PotPresenter>().FromInstance(_pot).AsSingle();
		Container.Bind<PressPresenter>().FromInstance(_press).AsSingle();
		Container.Bind<ItemGrabber>().FromInstance(_grabber).AsSingle();

		Container.Bind<SoulPhrasePresenter>().FromInstance(_phrasePresenter).AsSingle();
		Container.Bind<SoulFactory>().AsSingle().WithArguments(_datas, _soulsCount, _devilSoul);
		Container.Bind<SoulPresenterFactory>().AsSingle().WithArguments(_presenterPrefab);

		Container.Bind<Game>().AsSingle().WithArguments(_soulsPosition.position, _minimumScoreForReincornation);
	}
}
