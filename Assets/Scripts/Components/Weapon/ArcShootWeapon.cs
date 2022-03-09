using Components.Weapon;
using DI.Signals;
using UnityEngine;

public class ArcShootWeapon : Weapon {
	public ArcShootWeapon(WeaponConfig weaponConfig) {
		_weaponConfig = weaponConfig;
	}

	protected override void Shoot() {
		for (int i = 0; i < _weaponConfig.BulletCountPerCycle; i++) {
			var bullet = _bulletPool.GetItem().GetComponent<Bullet>();
			bullet.gameObject.SetActive(true);
			bullet.transform.SetParent(_bulletContainer);
			bullet.transform.position = _bulletStartTransform.position;
			bullet.Initialize(_gameConfig.BulletSpeed,
				Quaternion.AngleAxis(-67.5F + i * 22.5f, Vector3.up) * (_bulletStartTransform.forward.normalized));
			bullet.SetOutOfScreenAction(DeactivateBullet);
		}
	}

	private void DeactivateBullet(Bullet bullet) {
		_signalBus.TryFire(new DeactivateBulletSignal { Bullet = bullet });
	}
}