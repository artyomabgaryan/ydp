using System;
using System.Collections.Generic;
using Components.Weapon;
using DI.Signals;
using Pools;
using UnityEngine;
using Zenject;

namespace Controllers {
public class WeaponController : IInitializable, IDisposable, ITickable {
	private readonly PlayerGunView _playerGunView;
	private readonly SignalBus _signalBus;
	private readonly List<Weapon> _weaponList;
	private Weapon _currentWeapon;
	private bool _isWeaponActive;
	[Inject] protected readonly Pool<GameObject> _bulletPool;


	public WeaponController(PlayerGunView playerGunView, List<Weapon> weaponList,
		SignalBus signalBus) {
		_playerGunView = playerGunView;
		_weaponList = weaponList;
		_signalBus = signalBus;
	}

	public void Initialize() {
		_signalBus.Subscribe<UserTouchInputSignal>(OnUserTouchInput);
		_signalBus.Subscribe<WeaponChangedSignal>(OnWeaponChanged);
		_signalBus.Subscribe<DeactivateBulletSignal>(x => DeactivateBullet(x.Bullet));

		_weaponList.ForEach(x => {
			x.SetBulletStartPosition(_playerGunView.BulletStartTransform);
			x.SetPool(_bulletPool);
		});
		_currentWeapon = _weaponList[0];
	}

	public void Dispose() {
		_signalBus.Unsubscribe<UserTouchInputSignal>(OnUserTouchInput);
		_signalBus.Unsubscribe<WeaponChangedSignal>(OnWeaponChanged);
		_signalBus.TryUnsubscribe<DeactivateBulletSignal>(OnBulletHitTheEnemyShip);
		_weaponList.ForEach(x => x.Deactivate());
	}

	public void Tick() {
		var ray = new Ray(_playerGunView.BulletStartTransform.position, _playerGunView.BulletStartTransform.forward);
		if (Physics.Raycast(ray, out var castHitInfo)) { // 
			if (castHitInfo.transform.CompareTag("Enemy")) {
				Debug.DrawLine(_playerGunView.BulletStartTransform.position, castHitInfo.transform.position, Color.red,
					1);
				if (!_isWeaponActive) {
					ActivateWeapon();
				}
			}
			else {
				if (_isWeaponActive) DeactivateWeapon();
			}
		}
		else {
			if (_isWeaponActive) DeactivateWeapon();
		}
	}

	private void OnWeaponChanged(WeaponChangedSignal signal) {
		DeactivateWeapon();
		_currentWeapon = _weaponList[signal.Index];
	}

	private void ActivateWeapon() {
		_isWeaponActive = true;
		_currentWeapon.Activate();
	}

	private void DeactivateWeapon() {
		_isWeaponActive = false;
		_currentWeapon?.Deactivate();
	}


	private void OnBulletHitTheEnemyShip(DeactivateBulletSignal signal) {
		DeactivateBullet(signal.Bullet);
	}

	protected void DeactivateBullet(Bullet bullet) {
		bullet.gameObject.SetActive(false);
		_bulletPool.AddItem(bullet.gameObject);
	}

	private void OnUserTouchInput(UserTouchInputSignal signal) {
		_playerGunView.RotateTurret(signal.DeltaPosition.x * Time.deltaTime * 5);
	}
}
}