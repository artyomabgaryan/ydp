using System;
using DI.Signals;
using Views;
using Zenject;

namespace Controllers {
public class WeaponUIController : IInitializable, IDisposable {
	private readonly SignalBus _signalBus;
	private WeaponView _weaponView;

	public WeaponUIController(SignalBus signalBus, WeaponView weaponView) {
		_signalBus = signalBus;
		_weaponView = weaponView;
	}

	public void Initialize() {
		_weaponView.SetWeaponTypeButtonClickAction(OnWeaponChanged);
		_weaponView.DeactivateConditionalWeapons();

		_signalBus.Subscribe<ActivateWeaponSignal>(OnWeaponActivated);
	}

	public void Dispose() {
		_signalBus.Unsubscribe<ActivateWeaponSignal>(OnWeaponActivated);
	}

	private void OnWeaponActivated(ActivateWeaponSignal signal) {
		_weaponView.ActivateWeapon(signal.Index);
	}

	private void OnWeaponChanged(int index) {
		_signalBus.TryFire(new WeaponChangedSignal { Index = index });
	}
}
}