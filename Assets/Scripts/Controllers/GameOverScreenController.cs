using DI.Signals;
using Zenject;

public class GameOverScreenController : IInitializable {
	private GameOverScreenView _gameOverScreenView;
	private readonly SignalBus _signalBus;
	private IScoreService _scoreService;

	public GameOverScreenController(GameOverScreenView gameOverScreenView, SignalBus signalBus,
		IScoreService scoreService) {
		_signalBus = signalBus;
		_gameOverScreenView = gameOverScreenView;
		_scoreService = scoreService;
	}

	public void Initialize() {
		_gameOverScreenView.SetHomeScreenButtonClick(OnHomeScreenButtonClick);
		_gameOverScreenView.SetRestartButtonClickAction(OnRestartButtonClick);
		_gameOverScreenView.InitializeScore(_scoreService.GetHighScore(), _scoreService.GetCurrentScore());
	}

	private void OnHomeScreenButtonClick() {
		_signalBus.Fire(new LoadSceneSignal { SceneId = SceneType.Start });
	}

	private void OnRestartButtonClick() {
		_signalBus.Fire(new LoadSceneSignal { SceneId = SceneType.Main });
	}
}