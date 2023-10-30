using UnityEngine;

public class GameFinal : MonoBehaviour
{
	[SerializeField] private int _endSceneBuildIndex;
	[SerializeField] private Color _badEndColor;

	public static int SuccessfulReincornationsCount { get; private set; }
	public static int FailedReincornationsCount { get; private set; }
	public static bool DevilIsHappy { get; private set; }

	private bool _exit;

	private void OnEnable()
	{
		DevilIsHappy = true;
		_exit = false;
		SuccessfulReincornationsCount = 0;
		FailedReincornationsCount = 0;
		Game.OnSuccessedReincornation += Game_OnSuccessedReincornation;
		Game.OnFailedReincornation += Game_OnFailedReincornation;
		Game.OnGameFinishedSuccessful += Game_OnGameFinishedSuccessful;
		Game.OnGameFinishedFailed += Game_OnGameFinishedFailed;
	}
	private void OnDisable()
	{
		Game.OnSuccessedReincornation -= Game_OnSuccessedReincornation;
		Game.OnFailedReincornation -= Game_OnFailedReincornation;
		Game.OnGameFinishedSuccessful -= Game_OnGameFinishedSuccessful;
		Game.OnGameFinishedFailed -= Game_OnGameFinishedFailed;
	}

	private void Update()
	{
		if (_exit) return;
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			_exit = true;
			SceneTransitions.Instance.StartTransition(0, 1);
		}
	}

	private void Game_OnGameFinishedSuccessful()
	{
		_exit = true;
		SceneTransitions.Instance.StartTransition(1, _endSceneBuildIndex);
	}

	private void Game_OnGameFinishedFailed()
	{
		_exit = true;
		DevilIsHappy = false;
		SceneTransitions.Instance.SetMainImageColor(_badEndColor);
		SceneTransitions.Instance.StartTransition(1, _endSceneBuildIndex);
	}

	private void Game_OnFailedReincornation(SoulType arg1, SoulReincornationType arg2)
	{
		FailedReincornationsCount++;
	}

	private void Game_OnSuccessedReincornation(SoulType arg1, SoulReincornationType arg2)
	{
		SuccessfulReincornationsCount++;
	}
}