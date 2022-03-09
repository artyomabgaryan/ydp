using System;
using UnityEngine;
using Zenject;

namespace Components.Weapon {
public class Bullet : MonoBehaviour {
	private float _speed;
	private Vector3 _direction;
	private Action<Bullet> _onOutOfScreen;
	[Inject] private IBoundsProvider _boundsProvider;

	public void Initialize(float speed, Vector3 direction) {
		_speed = speed;
		_direction = direction;
		transform.up = -_direction;
	}
	
	public void SetOutOfScreenAction(Action<Bullet> onOutOfScreen) {
		_onOutOfScreen = onOutOfScreen;
	}
	
	private void Update() {
		transform.position += _direction * _speed * Time.deltaTime;
		if (!_boundsProvider.SafeBounds.Contains(transform.position)) {
			_onOutOfScreen?.Invoke(this);
		}
	}
}
}