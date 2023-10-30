using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PotPresenter : MonoBehaviour
{
	public ItemData[] Items => _containsItems.Distinct().ToArray();

    [SerializeField] private ParticleSystem _reincarnationParticles;
    [SerializeField] private ParticleSystem _putInParticles;
    [SerializeField] private ParticleSystem _liquidParticles;
    [SerializeField] private SpriteRenderer _liquidSpriteRenderer;
    [Space]
    [SerializeField] private ItemTrigger _itemTrigger;
	[Space]
	[SerializeField] private AudioClip _potSound;
	[SerializeField] private float _potSoundVolume;
	[Space]
	[SerializeField] private AudioClip _reincarnationSound;
	[SerializeField] private float _reincarnationSoundVolume;
	[Space]
	[SerializeField] private AudioClip _resetSound;
	[SerializeField] private float _resetSoundVolume;


	[Inject] private AudioSource _mainAudio;

    private Color _defaultLiquidColor;
    private List<ItemData> _containsItems = new();

    private Color _targetColor;

    private void Awake()
	{
        _defaultLiquidColor = _liquidSpriteRenderer.color;
		_targetColor = _defaultLiquidColor;
	}

	private void OnEnable() =>
		_itemTrigger.OnEnter += ItemTrigger_OnEnter;

	private void OnDisable() =>
		_itemTrigger.OnEnter -= ItemTrigger_OnEnter;

	private void Update()
	{
		SetLiquidColor(Color.Lerp(_liquidSpriteRenderer.color, _targetColor, 0.005f));
	}

	public void ResetPot()
    {
		_targetColor = _defaultLiquidColor;
		_containsItems.Clear();
	}

	public void PlayResetSound()
	{
		_mainAudio.PlayOneShot(_resetSound, _resetSoundVolume);
	}

    public void StartReincarnation()
	{
		_mainAudio.PlayOneShot(_reincarnationSound, _reincarnationSoundVolume);
		_reincarnationParticles.Play();
	}

    private void ItemTrigger_OnEnter(ItemPresenter item)
	{
		_putInParticles.Play();
		_targetColor = Color.Lerp(_targetColor, item.Data.LiquidColor, item.Data.LiquidAmount);
		_containsItems.Add(item.Data);
		_mainAudio.PlayOneShot(_potSound, _potSoundVolume);
		Destroy(item.gameObject);
	}

    private void SetLiquidColor(Color color)
	{
		var module = _liquidParticles.main;
		module.startColor = color;

		_liquidSpriteRenderer.color = color;
	}
}