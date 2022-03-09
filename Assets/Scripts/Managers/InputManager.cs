using DI.Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputManager : ITickable {
	private readonly SignalBus _signalBus;
	private bool _isUsersTouchOnScreen;

	public InputManager(SignalBus signalBus) {
		_signalBus = signalBus;
	}

	public void Tick() {
		if (Input.touchCount < 1) return;
		if (Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject()) {
			_isUsersTouchOnScreen = true;
		}

		if (Input.touches[0].phase is TouchPhase.Canceled or TouchPhase.Ended) {
			_isUsersTouchOnScreen = false;
		}

		if (Input.touches[0].phase == TouchPhase.Moved && _isUsersTouchOnScreen) {
			_signalBus.TryFire(new UserTouchInputSignal { DeltaPosition = Input.touches[0].deltaPosition });
		}
	}
}