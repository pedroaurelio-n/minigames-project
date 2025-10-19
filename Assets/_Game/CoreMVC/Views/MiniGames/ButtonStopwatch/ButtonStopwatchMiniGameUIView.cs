using System;
using TMPro;
using UnityEngine;

public class ButtonStopwatchMiniGameUIView : MonoBehaviour
{
    public event Action OnButtonClick;

    [field: SerializeField] public StopwatchEntryUIView EntryPrefab { get; private set; }
    [field: SerializeField] public Transform EntriesContainer { get; private set; }
    
    [SerializeField] ButtonUIComponent button;
    [SerializeField] TextMeshProUGUI timerText;

    void Awake ()
    {
        button.OnClick += () => OnButtonClick?.Invoke();
    }
    
    public void SetTimerText (string text) => timerText.text = text;
}