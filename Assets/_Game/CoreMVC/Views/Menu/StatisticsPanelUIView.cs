using System;
using UnityEngine;

public class StatisticsPanelUIView : MonoBehaviour
{
    public event Action OnBackButtonClick;
    
    [field: SerializeField] public Transform EntriesContainer { get; private set; }
    [field: SerializeField] public StatisticsEntryUIView StatisticsEntryPrefab { get; private set; }

    [SerializeField] ButtonUIComponent backButton;

    void Awake ()
    {
        backButton.OnClick += () => OnBackButtonClick?.Invoke();
    }
}