using DI.Signals;
using UnityEngine;
using Zenject;

namespace Managers {
public class EditorInputManager : MonoBehaviour {
	[Inject] private readonly SignalBus _signalBus;
	private bool _isUsersTouchOnScreen;
	private Vector3 _tempMousePosition;

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			_isUsersTouchOnScreen = true;
			_tempMousePosition = Input.mousePosition;
		}

		if (Input.GetMouseButtonUp(0)) {
			_isUsersTouchOnScreen = false;
		}
		
		if (_isUsersTouchOnScreen) {
			_signalBus.TryFire(new UserTouchInputSignal { DeltaPosition = Input.mousePosition - _tempMousePosition });
			_tempMousePosition = Input.mousePosition;
		}
	}
}
}