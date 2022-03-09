using TMPro;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI _playerHealthLabel;

	public void SetHealth(int health) {
		_playerHealthLabel.text = health.ToString();
	}
}