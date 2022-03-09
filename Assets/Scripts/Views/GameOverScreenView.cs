using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenView : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _currentScoreLabel;
    [SerializeField] private TextMeshProUGUI _highScoreLabel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeScreenButton;
    [SerializeField] private Image _background;

    public void SetRestartButtonClickAction(Action onRestartButtonClick) {
        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(() => {
            onRestartButtonClick?.Invoke();
        });
    }

    public void SetHomeScreenButtonClick(Action onHomeScreenButtonClick) {
        _homeScreenButton.onClick.RemoveAllListeners();
        _homeScreenButton.onClick.AddListener(() => {
            onHomeScreenButtonClick?.Invoke();
        });
    }
    
    public void InitializeScore(int highScore, int currentScore) {
        _highScoreLabel.text = "High Score\n" + highScore; //;
        _currentScoreLabel.text = "Current Score\n" + currentScore; //_scoreManager.GetCurrentScore();
    }

    private void Update() {
        var backgroundColor = _background.color;
        backgroundColor.a = .2f + 0.3f * Mathf.PerlinNoise(Time.time / 2, 0);
        _background.color = backgroundColor;
    }
}