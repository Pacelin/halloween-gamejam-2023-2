using UnityEngine;

public class SoulFactory
{
	private SoulData[] _datas;
	private int _currentSoul;
	private SoulData _devilData;
	private int _soulsCount;

	public SoulFactory(SoulData[] datas, int soulsCount, SoulData devilData)
	{
		for (int i = 0; i < datas.Length; i++)
		{
			for (int j = 0; j < datas.Length; j++)
			{
				var rIndex = Random.Range(0, datas.Length);
				var t = datas[rIndex];
				datas[rIndex] = datas[i];
				datas[i] = t;
			}
		}
		_datas = datas;
		_currentSoul = 0;
		_soulsCount = soulsCount;
		_devilData = devilData;
	}

	public SoulModel Create(out bool devil)
	{
		devil = false;
		if (_currentSoul == _soulsCount || _currentSoul == _datas.Length)
		{
			devil = true;
			return new SoulModel(_devilData);
		}

		return new SoulModel(_datas[_currentSoul++]);
	}
}