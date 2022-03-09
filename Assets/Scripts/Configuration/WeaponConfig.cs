using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Fire Config", menuName = "GameConfigs/WeaponFireConfig")]
public class WeaponConfig : ScriptableObject
{
	[SerializeField] private float _fireCycleDuration;
	[SerializeField] private int _bulletCountPerCycle;

	public float FireCycleDuration => _fireCycleDuration;
	public int BulletCountPerCycle => _bulletCountPerCycle;
}
