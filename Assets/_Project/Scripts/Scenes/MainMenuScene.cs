using UnityEngine;

public class MainMenuScene : MonoBehaviour
{
    [SerializeField] private TMP_Text_Button _playButton;
    [SerializeField] private TMP_Text_Button _exitButton;
	[SerializeField] private int _gameSceneBuildIndex;

	private void OnEnable()
	{
		_playButton.OnClick.AddListener(GoToPlayScene);
		_exitButton.OnClick.AddListener(Exit);
	}

	private void OnDisable()
	{
		_playButton.OnClick.RemoveListener(GoToPlayScene);
		_exitButton.OnClick.RemoveListener(Exit);
	}

	private void GoToPlayScene() =>
		SceneTransitions.Instance.StartTransition(0, _gameSceneBuildIndex);
	private void Exit() =>
		Application.Quit();
}