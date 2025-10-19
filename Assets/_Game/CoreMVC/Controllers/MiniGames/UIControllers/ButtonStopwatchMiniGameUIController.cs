using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ButtonStopwatchMiniGameUIController : BaseMiniGameUIController
{
    public event Action OnButtonClick;
    
    public ButtonStopwatchMiniGameUIView UIView { get; private set; }
    
    public override void Setup (SceneUIView sceneUIView)
    {
        base.Setup(sceneUIView);

        UIView = Object.Instantiate(
            Resources.Load<ButtonStopwatchMiniGameUIView>("ButtonStopwatchMiniGameUIView"),
            SceneUIView.PriorityHUD
        );
        
        AddViewListeners();
    }

    public void SyncTimer (float timer)
    {
        UIView.SetTimerText(timer.ToString("F2"));
    }

    public void SyncEntries (List<float> entryValues, List<StopwatchEntryUIView> entryViews)
    {
        for (int i = 0; i < entryViews.Count; i++)
            entryViews[i].SetTimeText(entryValues[i].ToString("F2"));
    }

    void AddViewListeners ()
    {
        UIView.OnButtonClick += HandleButtonClick;
    }
    
    void RemoveViewListeners ()
    {
        UIView.OnButtonClick -= HandleButtonClick;
    }
    
    void HandleButtonClick () => OnButtonClick?.Invoke();

    public override void Dispose ()
    {
        RemoveViewListeners();
    }
}