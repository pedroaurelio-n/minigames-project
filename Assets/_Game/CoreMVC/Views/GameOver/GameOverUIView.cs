using System;
using TMPro;
using UnityEngine;

public class GameOverUIView : MenuUIView
{
    public event Action OnRestartButtonClick;
    public event Action OnMenuButtonClick;
    
    [SerializeField] ButtonUIComponent restartButton;
    [SerializeField] ButtonUIComponent menuButton;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    void Awake ()
    {
        restartButton.OnClick += () => OnRestartButtonClick?.Invoke();
        menuButton.OnClick += () => OnMenuButtonClick?.Invoke();
    }
    
    public void SetLivesText (string text) => livesText.text = text;
    
    public void SetScoreText (string text) => scoreText.text = text;

    public void SetHighScoreText (string text) => highScoreText.text = text;
}