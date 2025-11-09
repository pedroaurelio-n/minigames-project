using System;
using System.Collections.Generic;

public class ButtonStopwatchMiniGameUIController : BaseMiniGameUIController<ButtonStopwatchMiniGameUIView>
{
    public event Action OnButtonClick;
    
    public override void Setup (SceneUIView sceneUIView)
    {
        base.Setup(sceneUIView);
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

    protected override void AddViewListeners ()
    {
        UIView.OnButtonClick += HandleButtonClick;
    }
    
    protected override void RemoveViewListeners ()
    {
        UIView.OnButtonClick -= HandleButtonClick;
    }
    
    void HandleButtonClick () => OnButtonClick?.Invoke();

    public override void Dispose ()
    {
        RemoveViewListeners();
    }
}