using UnityEngine;

public class PlayerGunView : MonoBehaviour {
	[SerializeField] private Transform _bulletStartTransform;
	[SerializeField] private Transform _turret;

	public Transform BulletStartTransform => _bulletStartTransform;

	public void RotateTurret(float angle) {
		var rotationEulerAngles = _turret.localRotation.eulerAngles;
		if (Mathf.Abs(rotationEulerAngles.y) > 180) {
			rotationEulerAngles.y -= 360;
		}

		if (Mathf.Abs(rotationEulerAngles.y + angle) < 90) {
			_turret.Rotate(0, 0, angle, Space.Self);
		}
	}
}