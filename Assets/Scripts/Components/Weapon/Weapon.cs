using System;
using Pools;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class Weapon : IDisposable {
	[Inject] protected Transform _bulletContainer;
	[Inject] protected GameConfig _gameConfig;
	[Inject] protected SignalBus _signalBus;
	protected Transform _bulletStartTransform;
	protected WeaponConfig _weaponConfig;
	protected IDisposable _shootTimer;
	protected Pool<GameObject> _bulletPool;
	
	public virtual void SetBulletStartPosition(Transform bulletStartTransform) {
		_bulletStartTransform = bulletStartTransform;
	}

	public virtual void Activate() {
		_shootTimer = Observable.Interval(TimeSpan.FromSeconds(_weaponConfig.FireCycleDuration))
			.Subscribe(_ => Shoot());
	}

	public virtual void Deactivate() {
		_shootTimer?.Dispose();
	}

	protected abstract void Shoot();

	public virtual void Dispose() {
		_shootTimer?.Dispose();
	}

	public void SetPool(Pool<GameObject> bulletPool) {
		_bulletPool = bulletPool;
	}
}