using System;
using TMPro;
using UnityEngine;

public class MainMenuPanelUIView : MonoBehaviour
{
    public event Action OnPlayButtonClick;
    public event Action OnLevelSelectButtonClick;
    
    [SerializeField] ButtonUIComponent playButton;
    [SerializeField] ButtonUIComponent levelSelectButton;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI userText;

    void Awake ()
    {
        playButton.OnClick += () => OnPlayButtonClick?.Invoke();
        levelSelectButton.OnClick += () => OnLevelSelectButtonClick?.Invoke();
    }

    public void SetHighScoreText (string text) => highScoreText.text = text;

    public void SetUserText (bool active, string text = "")
    {
        userText.gameObject.SetActive(active);
        userText.text = text;
    }
}