using Components.Weapon;
using DI.Signals;


public class SingleShootWeapon : Weapon {
	public SingleShootWeapon(WeaponConfig weaponConfig) {
		_weaponConfig = weaponConfig;
	}

	protected override void Shoot() {
		var bullet = _bulletPool.GetItem().GetComponent<Bullet>();
		bullet.gameObject.SetActive(true);
		bullet.transform.SetParent(_bulletContainer);
		bullet.transform.position = _bulletStartTransform.position;
		bullet.Initialize(_gameConfig.BulletSpeed, _bulletStartTransform.forward);
		bullet.SetOutOfScreenAction(DeactivateBullet);
	}

	private void DeactivateBullet(Bullet bullet) {
		_signalBus.TryFire(new DeactivateBulletSignal { Bullet = bullet });
	}
}