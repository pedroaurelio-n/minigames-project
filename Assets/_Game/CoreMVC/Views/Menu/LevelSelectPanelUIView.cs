using System;
using UnityEngine;

public class LevelSelectPanelUIView : MonoBehaviour
{
    public event Action OnBackButtonClick;
    
    [field: SerializeField] public Transform ButtonContainer { get; private set; }
    [field: SerializeField] public LevelSelectButtonUIView LevelSelectButtonPrefab { get; private set; }

    [SerializeField] ButtonUIComponent backButton;

    void Awake ()
    {
        backButton.OnClick += () => OnBackButtonClick?.Invoke();
    }
}