using System;
using TMPro;
using UnityEngine;

public class MainMenuUIView : MenuUIView
{
    public event Action OnPlayButtonClick;
    
    [SerializeField] ButtonUIComponent playButton;
    [SerializeField] TextMeshProUGUI highScoreText;

    void Awake ()
    {
        playButton.OnClick += () => OnPlayButtonClick?.Invoke();
    }

    public void SetHighScoreText (string text) => highScoreText.text = text;
}