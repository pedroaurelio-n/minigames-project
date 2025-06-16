using System;
using TMPro;
using UnityEngine;

public class ButtonMashMiniGameUIView : MonoBehaviour
{
    public event Action OnLeftButtonClick;
    public event Action OnRightButtonClick;
    
    [SerializeField] ButtonUIComponent leftButton;
    [SerializeField] ButtonUIComponent rightButton;
    [SerializeField] TextMeshProUGUI leftButtonText;
    [SerializeField] TextMeshProUGUI rightButtonText;

    void Awake ()
    {
        leftButton.OnClick += () => OnLeftButtonClick?.Invoke();
        rightButton.OnClick += () => OnRightButtonClick?.Invoke();
    }
    
    public void SetLeftButtonText (string text) => leftButtonText.text = text;
    
    public void SetRightButtonText (string text) => rightButtonText.text = text;
}