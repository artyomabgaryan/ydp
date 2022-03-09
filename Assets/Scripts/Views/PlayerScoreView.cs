using TMPro;
using UnityEngine;

public class PlayerScoreView : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI _playerScoreLabel;

	public void SetScore(int score) {
		_playerScoreLabel.text = score.ToString();
	}
}