using UnityEngine;
using Zenject;

public class BootScene : MonoBehaviour
{
	[SerializeField] private TMP_Text_ThicknessTween _textTween;
	[SerializeField] private TMP_Text_ThicknessTween _textReturnTween;
	[SerializeField] private Animation_Tween _controlsTween;
	[Space]
	[SerializeField] private AudioClip _controlsAudio;
	[SerializeField] private float _volume = 0.8f;
	[Space]
	[SerializeField] private int _nextSceneBuildIndex;


	[Inject] private AudioSource _audioSource;

	private void OnEnable() =>
		_controlsTween.OnAnimationEvent += ControlsTween_OnAnimationEvent;
	private void OnDisable() =>
		_controlsTween.OnAnimationEvent -= ControlsTween_OnAnimationEvent;

	private void Start()
	{
		_textTween.Play(0.5f, () => 
			_textReturnTween.Play(0.5f, () =>
				_controlsTween.Play(0.2f, () =>
					SceneTransitions.Instance.StartTransition(0.5f, _nextSceneBuildIndex))));
	}

	private void ControlsTween_OnAnimationEvent(int obj)
	{
		_audioSource.PlayOneShot(_controlsAudio, _volume);
	}
}