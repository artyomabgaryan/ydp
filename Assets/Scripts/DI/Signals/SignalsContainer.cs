using Components.Weapon;
using UnityEngine;

namespace DI.Signals {
	public struct LoadSceneSignal { public SceneType SceneId; }
	public struct UserTouchInputSignal { public Vector2 DeltaPosition; }
	public struct PlayZoneGotHitSignal { public Enemy Enemy; }
	public struct ActivateWeaponSignal { public int Index; }
	public struct WeaponChangedSignal { public int Index; }
	public struct EnemyShipDestroyedSignal { }
	public struct DeactivateBulletSignal {
		public Bullet Bullet;
	}
}