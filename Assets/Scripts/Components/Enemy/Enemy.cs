using System;
using Components.Weapon;
using DI.Signals;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour {
	private Action<Enemy> _onCollidedWithPlayerZone;
	private Action<Enemy> _onDestroyedByPlayer;
	private Action<Enemy> _onOutOfScreen;
	private int _lifeCount;
	private float _speed;
	private Vector3 _direction;
	[Inject] private IBoundsProvider _boundsProvider;
	[Inject] private SignalBus _signalBus;

	public void Initialize(int lifeCount, float speed, Vector3 direction) {
		_lifeCount = lifeCount;
		_speed = speed;
		_direction = direction;
		transform.up = -_direction;
	}

	public void SetCollidedWithPlayerZoneAction(Action<Enemy> onCollidedWithPlayerZone) {
		_onCollidedWithPlayerZone = onCollidedWithPlayerZone;
	}

	public void SetDestroyedByPlayerAction(Action<Enemy> onDestroyedByPlayer) {
		_onDestroyedByPlayer = onDestroyedByPlayer;
	}

	public void SetOutOfScreenAction(Action<Enemy> onOutOfScreen) {
		_onOutOfScreen = onOutOfScreen;
	}

	private void Update() {
		transform.position += _direction * _speed * Time.deltaTime;
		if (!_boundsProvider.SafeBounds.Contains(transform.position)) {
			_onOutOfScreen?.Invoke(this);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("PlayerZone")) {
			_onCollidedWithPlayerZone?.Invoke(this);
		}
		else if (other.gameObject.CompareTag("Bullet")) {
			_signalBus.TryFire(new DeactivateBulletSignal
				{ Bullet = other.gameObject.GetComponent<Bullet>() });
			if (_lifeCount == 0) {
				_onDestroyedByPlayer?.Invoke(this);
			}
			else {
				_lifeCount--;
			}
			
		}
	}
}