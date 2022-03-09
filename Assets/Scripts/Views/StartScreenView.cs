using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenView : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI _highScoreLabel;
	[SerializeField] private TextMeshProUGUI _tapToStartButtonLabel;
	[SerializeField] private Button _tapToStartButton;
	[SerializeField] private Image _background;

	public void Initialize() {
		InitializeAnimation();
	}

	public void SetStartButtonClickAction(Action onStartButtonClick) {
		_tapToStartButton.onClick.RemoveAllListeners();
		_tapToStartButton.onClick.AddListener(() => { onStartButtonClick?.Invoke(); });
	}

	public void InitializeScore(int highScore) {
		_highScoreLabel.text = "High Score\n" + highScore;
	}

	private void InitializeAnimation() {
		_tapToStartButtonLabel.DOFade(.2f, 1).SetLoops(-1, LoopType.Yoyo);
	}

	private void Update() {
		var backgroundColor = _background.color;
		backgroundColor.a = 0.3f * Mathf.PerlinNoise(Time.time / 2, 0);
		_background.color = backgroundColor;
	}
}