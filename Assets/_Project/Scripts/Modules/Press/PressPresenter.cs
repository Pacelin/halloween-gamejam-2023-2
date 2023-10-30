using System.Linq;
using UnityEngine;
using Zenject;

public class PressPresenter : MonoBehaviour
{
	[SerializeField] private ItemPressRecipe[] _recipes;
	[SerializeField] private Transform _itemPlace;
	[Space]
	[SerializeField] private ItemTrigger _trigger;
	[Space]
	[SerializeField] private ParticleSystem _kickParticles;
	[SerializeField] private Animation _closeAnimation;
	[Space]
	[SerializeField] private AudioClip _closeClip;
	[SerializeField] private float _closeClipVolume;

	[Inject] private AudioSource _mainAudioSource;

	private ItemPresenter _itemIn;

	public void ClosePress() =>
		_closeAnimation.Play();

	private void PastItem()
	{
		if (_trigger.ItemIn == null) return;
		_itemIn = _trigger.ItemIn;
		_itemIn.SetTrigger();
		_itemIn.transform.position = _itemPlace.position;
		_itemIn.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
	}

	private void SwitchItem()
	{
		if (_itemIn == null) return;

		var recipe = _recipes.FirstOrDefault(r => r.FromItem != null && r.FromItem.Data == _itemIn.Data);
		if (recipe != null)
		{
			var pos = _itemIn.transform.position;
			var rot = _itemIn.transform.rotation;
			Destroy(_itemIn.gameObject);
			var newItem = Instantiate(recipe.ToItem, pos, rot);
			newItem.SetTrigger();
		}
		else
		{
			Destroy(_itemIn.gameObject);
		}
		_itemIn = null;
	}

	private void PlaySound() =>
		_mainAudioSource.PlayOneShot(_closeClip, _closeClipVolume);
	private void BurstParticles() =>
		_kickParticles.Play();
}