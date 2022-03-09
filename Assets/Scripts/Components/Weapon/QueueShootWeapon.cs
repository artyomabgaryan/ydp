using System;
using Components.Weapon;
using DI.Signals;
using UniRx;


public class QueueShootWeapon : Weapon {
	protected IDisposable _qShootTimer;
	private int _shotCounter;

	public QueueShootWeapon(WeaponConfig weaponConfig) {
		_weaponConfig = weaponConfig;
	}

	public override void Deactivate() {
		base.Deactivate();
		_qShootTimer?.Dispose();
	}

	protected override void Shoot() {
		_shotCounter = 0;
		_qShootTimer = Observable
			.Interval(TimeSpan.FromSeconds(_weaponConfig.FireCycleDuration / _weaponConfig.BulletCountPerCycle))
			.Subscribe(_ => QueueShoot());
	}

	private void QueueShoot() {
		var bullet = _bulletPool.GetItem().GetComponent<Bullet>();
		bullet.gameObject.SetActive(true);
		bullet.transform.SetParent(_bulletContainer);
		bullet.transform.position = _bulletStartTransform.position;
		bullet.Initialize(_gameConfig.BulletSpeed, _bulletStartTransform.forward);
		bullet.SetOutOfScreenAction(DeactivateBullet);
		_shotCounter++;
		if (_shotCounter == _weaponConfig.BulletCountPerCycle) _qShootTimer.Dispose();
	}

	private void DeactivateBullet(Bullet bullet) {
		_signalBus.TryFire(new DeactivateBulletSignal { Bullet = bullet });
	}
}