using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views {
public class WeaponView : MonoBehaviour {
	[SerializeField] private Button[] _buttons;

	public void SetWeaponTypeButtonClickAction(Action<int> onWeaponTypeButtonClickAction) {
		for (int i = 0; i < _buttons.Length; i++) {
			_buttons[i].onClick.RemoveAllListeners();
			var index = i;
			_buttons[i].onClick.AddListener(() => onWeaponTypeButtonClickAction.Invoke(index));
		}
	}

	public void ActivateWeapon(int index) {
		_buttons[index].interactable = true;
	}
	
	public void DeactivateConditionalWeapons() {
		for (int i = 1; i < _buttons.Length; i++) {
			_buttons[i].interactable = false;
		}
	}
}
}