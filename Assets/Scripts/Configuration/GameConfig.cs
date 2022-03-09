using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "GameConfigs/Game Config")]
public class GameConfig : ScriptableObject {
	[Header("Timing")]
	[SerializeField] private int _enemySpawnTime;
	[Header("Speed")] 
	[SerializeField] private Vector2 _enemySpeedMinMax;
	[SerializeField] private float _bulletSpeed;
	[Header("Health")] 
	[SerializeField] private int _minEnemyHealth;
	[SerializeField] private int _maxEnemyHealth;
	[SerializeField] private int _playHealth;

	[Header("Weapon Activation Score")] 
	[SerializeField] private int _secondWeaponActivationScore;
	[SerializeField] private int _thirdWeaponActivationScore;

	public int PlayerHealth => _playHealth;
	public int RandomEnemyHealth => Random.Range(_minEnemyHealth, _maxEnemyHealth);
	public float RandomEnemySpeed => Random.Range(_enemySpeedMinMax.x, _enemySpeedMinMax.y);
	public int SecondWeaponActivationScore => _secondWeaponActivationScore;
	public int ThirdWeaponActivationScore => _thirdWeaponActivationScore;

	public float BulletSpeed => _bulletSpeed;

	public int EnemySpawnTime => _enemySpawnTime;
}