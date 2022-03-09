using System;
using DI.Signals;
using Zenject;

namespace Controllers {
	public class ScoreController : IInitializable, IDisposable {
		private readonly PlayerScoreView _playerScoreView;
		private readonly IScoreService _scoreService;
		private readonly SignalBus _signalBus;

		public ScoreController(PlayerScoreView playerScoreView, IScoreService scoreService, SignalBus signalBus) {
			_playerScoreView = playerScoreView;
			_scoreService = scoreService;
			_signalBus = signalBus;
		}

		public void Initialize() {
			_scoreService.SetCurrentScore(0);
			InitializeEvents();
		}

		public void Dispose() {
			_signalBus.TryUnsubscribe<EnemyShipDestroyedSignal>(OnEnemyShipDestroyed);
			_signalBus.TryUnsubscribe<LoadSceneSignal>(OnLoadScene);
		}

		private void InitializeEvents() {
			_signalBus.Subscribe<EnemyShipDestroyedSignal>(OnEnemyShipDestroyed);
			_signalBus.Subscribe<LoadSceneSignal>(OnLoadScene);
		}

		private void OnLoadScene(LoadSceneSignal signal) {
			if (signal.SceneId == SceneType.Main) {
				_scoreService.SetCurrentScore(0);
			}
		}

		private void OnEnemyShipDestroyed() {
			var currentScore = _scoreService.GetCurrentScore();
			currentScore++;
			_scoreService.SetCurrentScore(currentScore);
			_playerScoreView.SetScore(currentScore);
		}
	}
}