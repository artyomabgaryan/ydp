using DI.Signals;
using Zenject;

public class StartScreenController : IInitializable {
	private StartScreenView _startScreenView;
	private readonly SignalBus _signalBus;
	private IScoreService _scoreService;

	public StartScreenController(StartScreenView startScreenView, SignalBus signalBus,
		IScoreService scoreService) {
		_startScreenView = startScreenView;
		_signalBus = signalBus;
		_scoreService = scoreService;
	}

	public void Initialize() {
		_startScreenView.Initialize();
		_startScreenView.SetStartButtonClickAction(OnStartButtonClick);
		_startScreenView.InitializeScore(_scoreService.GetHighScore());
	}

	private void OnStartButtonClick() {
		_signalBus.Fire(new LoadSceneSignal { SceneId = SceneType.Main });
	}
}