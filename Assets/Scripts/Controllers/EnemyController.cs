using System;
using DI.Signals;
using Pools;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

namespace Controllers {
public class EnemyController : IInitializable, IDisposable {
	private readonly Pool<GameObject> _enemyPool;
	private readonly Transform _container;
	private readonly SignalBus _signalBus;
	private IDisposable _spawnTimerHandler;

	[Inject] private IBoundsProvider _boundsProvider;
	[Inject] private GameConfig _gameConfig;

	public EnemyController(Pool<GameObject> enemyPool, Transform container, SignalBus signalBus) {
		_enemyPool = enemyPool;
		_container = container;
		_signalBus = signalBus;
	}

	public void Initialize() {
		_spawnTimerHandler = Observable.Interval(TimeSpan.FromSeconds(_gameConfig.EnemySpawnTime))
			.Subscribe(_ => OnSpawnEnemy());
	}

	public void Dispose() {
		_spawnTimerHandler.Dispose();
	}

	private void OnSpawnEnemy() {
		var enemy = _enemyPool.GetItem().GetComponent<Enemy>();
		enemy.gameObject.SetActive(true);
		enemy.transform.SetParent(_container);
		enemy.transform.position = GetSpawnPosition();
		var direction = new Vector3(0, -4f, 9.3f) - enemy.transform.position;  
		enemy.Initialize(_gameConfig.RandomEnemyHealth, _gameConfig.RandomEnemySpeed,
			direction);
		enemy.SetDestroyedByPlayerAction(OnDestroyedByPlayer);
		enemy.SetOutOfScreenAction(OnOutOfScreen);
		enemy.SetCollidedWithPlayerZoneAction(OnCollidedWithPlayerZone);
	}
	
	private void OnCollidedWithPlayerZone(Enemy enemy) {
		DeactivateEnemy(enemy);
		_signalBus.TryFire<PlayZoneGotHitSignal>();
	}

	private void OnOutOfScreen(Enemy enemy) {
		DeactivateEnemy(enemy);
	}

	private void OnDestroyedByPlayer(Enemy enemy) {
		DeactivateEnemy(enemy);
		_signalBus.TryFire<EnemyShipDestroyedSignal>();
	}

	private void DeactivateEnemy(Enemy enemy) {
		var enemyGameObject = enemy.gameObject;
		enemyGameObject.SetActive(false);
		_enemyPool.AddItem(enemyGameObject);
	}

	private Vector3 GetSpawnPosition() {
		var safeBounds = _boundsProvider.SafeBounds;
		safeBounds.Expand(-1);
		var spawnYPos = safeBounds.max.y;
		var spawnXPos = Random.Range(safeBounds.min.x, safeBounds.max.x);
		return new Vector3(spawnXPos, spawnYPos, 9.3f);
	}
}
}