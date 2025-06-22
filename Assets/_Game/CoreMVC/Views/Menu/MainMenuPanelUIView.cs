using System;
using TMPro;
using UnityEngine;

public class MainMenuPanelUIView : MonoBehaviour
{
    public event Action OnPlayButtonClick;
    public event Action OnLevelSelectButtonClick;
    public event Action OnStatisticsButtonClick;
    
    [SerializeField] ButtonUIComponent playButton;
    [SerializeField] ButtonUIComponent levelSelectButton;
    [SerializeField] ButtonUIComponent statisticsButton;
    [SerializeField] TextMeshProUGUI highScoreText;

    void Awake ()
    {
        playButton.OnClick += () => OnPlayButtonClick?.Invoke();
        levelSelectButton.OnClick += () => OnLevelSelectButtonClick?.Invoke();
        statisticsButton.OnClick += () => OnStatisticsButtonClick?.Invoke();
    }

    public void SetHighScoreText (string text) => highScoreText.text = text;
}