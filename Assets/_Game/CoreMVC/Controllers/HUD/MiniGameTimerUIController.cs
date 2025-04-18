using System;

public class MiniGameTimerUIController : IDisposable
{
    readonly IMiniGameTimerModel _miniGameTimerModel;
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly MiniGameTimerUIView _view;

    public MiniGameTimerUIController (
        IMiniGameTimerModel miniGameTimerModel,
        IMiniGameManagerModel miniGameManagerModel,
        MiniGameTimerUIView view
    )
    {
        _miniGameTimerModel = miniGameTimerModel;
        _miniGameManagerModel = miniGameManagerModel;
        _view = view;
    }

    public void Initialize ()
    {
        if (_miniGameManagerModel.ActiveMiniGame == null)
            return;
        AddListeners();
    }

    void AddListeners ()
    {
        _miniGameTimerModel.OnTimerChanged += HandleTimerChanged;
    }

    void RemoveListeners ()
    {
        _miniGameTimerModel.OnTimerChanged -= HandleTimerChanged;
    }

    void HandleTimerChanged (float current, float max) => _view.FillBar.SetFillAmount(current, max);

    public void Dispose ()
    {
        if (_miniGameManagerModel.ActiveMiniGame == null)
            return;
        RemoveListeners();
    }
}