using System;
using DI.Signals;
using Zenject;

public class HealthController : IInitializable, IDisposable {
	private readonly PlayerHealthView _playerHealthView;
	private readonly GameConfig _gameConfig;
	private readonly SignalBus _signalBus;
	private int _playerHealth;

	public HealthController(PlayerHealthView playerHealthView, GameConfig gameConfig, SignalBus signalBus) {
		_playerHealthView = playerHealthView;
		_gameConfig = gameConfig;
		_signalBus = signalBus;
	}

	public void Initialize() {
		_playerHealth = _gameConfig.PlayerHealth;
		_playerHealthView.SetHealth(_playerHealth);
		_signalBus.Subscribe<PlayZoneGotHitSignal>(OnPlayerZoneGotHit);
	}

	private void OnPlayerZoneGotHit() {
		_playerHealth--;
		_playerHealthView.SetHealth(_playerHealth);
		if (_playerHealth == 0) {
			_signalBus.Fire(new LoadSceneSignal {SceneId = SceneType.GameOver});
		}
	}

	public void Dispose() {
		_signalBus.Unsubscribe<PlayZoneGotHitSignal>(OnPlayerZoneGotHit);
	}
}