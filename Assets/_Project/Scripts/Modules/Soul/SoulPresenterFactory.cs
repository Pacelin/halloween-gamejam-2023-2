using UnityEngine;

public class SoulPresenterFactory
{
	private SoulPresenter _prefab;

	public SoulPresenterFactory(SoulPresenter prefab) =>
		_prefab = prefab;

	public SoulPresenter Create(SoulModel param)
	{
		var presenter = Object.Instantiate(_prefab);
		presenter.SetColor(param.Color);
		return presenter;
	}
}