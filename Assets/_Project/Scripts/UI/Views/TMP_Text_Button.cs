using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Zenject;

public class TMP_Text_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
{
	public UnityEvent OnClick = new UnityEvent();
	public UnityEvent OnMouseEnter = new UnityEvent();
	public UnityEvent OnMouseExit = new UnityEvent();

	[SerializeField] private TMP_Text _target;
	[Space]
	[SerializeField] private Color _hoverGlowColor;
	[SerializeField] private Color _pressedGlowColor;
	[Space]
	[SerializeField] private float _hoverScale;
	[SerializeField] private float _pressedScale;
	[Space]
	[SerializeField] private AudioClip _onClickClip;
	[SerializeField] private float _onClickClipVolume;
	[SerializeField] private AudioClip _onMouseEnterClip;
	[SerializeField] private float _onMouseEnterClipVolume;

	[Inject] private AudioSource _mainAudioSource;

	private Material _textMaterial;
	private bool _pressed;
	private bool _hover;

	private const string GLOW_ON_KEYWORD = "GLOW_ON";
	private const string GLOW_COLOR_OPTION = "_GlowColor";

	private void Awake()
	{
		_textMaterial = Instantiate(_target.fontMaterial);
		_target.fontMaterial = _textMaterial;
	}

	private void UpdateMaterial()
	{
		if (!_pressed && !_hover)
		{
			_textMaterial.SetKeyword(new LocalKeyword(_textMaterial.shader, GLOW_ON_KEYWORD), false);
			_target.transform.localScale = new Vector3(1, 1, 1);
		}
		else if (_pressed)
		{
			_textMaterial.SetKeyword(new LocalKeyword(_textMaterial.shader, GLOW_ON_KEYWORD), true);
			_textMaterial.SetColor(GLOW_COLOR_OPTION, _pressedGlowColor);
			_target.transform.localScale = new Vector3(_pressedScale, _pressedScale, 1);
		}
		else if (_hover)
		{
			_textMaterial.SetKeyword(new LocalKeyword(_textMaterial.shader, GLOW_ON_KEYWORD), true);
			_textMaterial.SetColor(GLOW_COLOR_OPTION, _hoverGlowColor);
			_target.transform.localScale = new Vector3(_hoverScale, _hoverScale, 1);
		}
	}

	#region Events

	public void OnPointerEnter(PointerEventData eventData)
	{
		_hover = true;
		_mainAudioSource.PlayOneShot(_onMouseEnterClip, _onMouseEnterClipVolume);
		UpdateMaterial();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_hover = false;
		UpdateMaterial();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_pressed = true;
		UpdateMaterial();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_pressed = false;
		UpdateMaterial();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick.Invoke();
		_mainAudioSource.PlayOneShot(_onClickClip, _onClickClipVolume);
	}
	#endregion
}