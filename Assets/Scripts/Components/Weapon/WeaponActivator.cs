using System;
using DI.Signals;
using Zenject;

namespace Components.Weapon {
public class WeaponActivator : IInitializable, IDisposable {
	private readonly SignalBus _signalBus;
	private readonly GameConfig _gameConfig;
	private int _scoreCounter;

	public WeaponActivator(SignalBus signalBus, GameConfig gameConfig) {
		_signalBus = signalBus;
		_gameConfig = gameConfig;
	}

	public void Initialize() {
		_signalBus.Subscribe<EnemyShipDestroyedSignal>(OnEnemyShipDestroyed);
	}

	public void Dispose() {
		_signalBus.Unsubscribe<EnemyShipDestroyedSignal>(OnEnemyShipDestroyed);
	}

	private void OnEnemyShipDestroyed() {
		_scoreCounter++;

		if (_scoreCounter == _gameConfig.SecondWeaponActivationScore) {
			_signalBus.TryFire(new ActivateWeaponSignal { Index = 1 });
		}
		
		if (_scoreCounter == _gameConfig.ThirdWeaponActivationScore) {
			_signalBus.TryFire(new ActivateWeaponSignal { Index = 2 });
		}
	}
}
}