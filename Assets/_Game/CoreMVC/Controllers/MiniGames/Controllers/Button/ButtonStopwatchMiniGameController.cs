using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStopwatchMiniGameController : BaseMiniGameController
{
    protected override MiniGameType MiniGameType => MiniGameType.ButtonStopwatch;
    
    IButtonStopwatchMiniGameModel MiniGameModel => _miniGameManagerModel.ActiveMiniGame as IButtonStopwatchMiniGameModel;
    ButtonStopwatchMiniGameUIController MiniGameUIController => UIController as ButtonStopwatchMiniGameUIController;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly PoolableViewFactory _viewFactory;
    readonly ButtonStopwatchMiniGameOptions _options;
    readonly UniqueCoroutine _timerCoroutine;
    readonly List<float> _stopwatchEntryValues = new();
    readonly List<StopwatchEntryUIView> _stopwatchEntryViews = new();

    float _timer = 0;
    bool _success;

    public ButtonStopwatchMiniGameController (
        IMiniGameManagerModel miniGameManagerModel,
        SceneView sceneView,
        SceneUIView sceneUIView,
        PoolableViewFactory viewFactory,
        ButtonStopwatchMiniGameOptions options,
        ICoroutineRunner coroutineRunner
    ) : base(miniGameManagerModel, sceneView, sceneUIView)
    {
        _miniGameManagerModel = miniGameManagerModel;
        _viewFactory = viewFactory;
        _options = options;
        _timerCoroutine = new UniqueCoroutine(coroutineRunner);
    }

    public override void Initialize ()
    {
        if (_miniGameManagerModel.ActiveMiniGameType != MiniGameType)
            return;

        base.Initialize();
    }

    protected override void SetupMiniGame ()
    {
        UIController = new ButtonStopwatchMiniGameUIController();
        base.SetupMiniGame();
        
        _viewFactory.SetupPool(MiniGameUIController.UIView.EntryPrefab);
        AddUIListeners();
        
        _timerCoroutine.Start(TimerCoroutine());
    }

    protected override bool CheckWinCondition (bool timerEnded)
    {
        return _success;
    }

    protected override bool CheckFailCondition ()
    {
        if (!_success)
        {
            foreach (StopwatchEntryUIView entry in _stopwatchEntryViews)
                entry.SetSuccessful(false);
            MiniGameModel.ForceFailure();
        }
        return !_success;
    }

    void AddUIListeners ()
    {
        MiniGameUIController.OnButtonClick += HandleButtonClick;
    }
    
    void RemoveUIListeners ()
    {
        MiniGameUIController.OnButtonClick -= HandleButtonClick;
    }

    void HandleButtonClick ()
    {
        if (_stopwatchEntryValues.Count >= MiniGameModel.MaxTries)
        {
            if (!CheckWinCondition(false))
                CheckFailCondition();
            return;
        }

        float currentTimer = _timer;
        int closestInt = Mathf.RoundToInt(currentTimer);
        
        _stopwatchEntryValues.Add(currentTimer);
        StopwatchEntryUIView entryUIView =
            _viewFactory.GetView<StopwatchEntryUIView>(MiniGameUIController.UIView.EntriesContainer);
        entryUIView.ResetColor();
        _stopwatchEntryViews.Add(entryUIView);
        
        MiniGameUIController.SyncEntries(_stopwatchEntryValues, _stopwatchEntryViews);
        MiniGameUIController.SyncTimer(_timer);

        if (Mathf.Abs(closestInt - currentTimer) < _options.SuccessThreshold)
            _success = true;
        
        if (CheckWinCondition(false))
        {
            MiniGameModel.Complete();
            entryUIView.SetSuccessful(true);
        }
        else
        {
            if (_stopwatchEntryValues.Count >= MiniGameModel.MaxTries)
                CheckFailCondition();
        }
    }

    IEnumerator TimerCoroutine ()
    {
        while (true)
        {
            if (!IsActive)
            {
                yield return null;
                continue;
            }

            _timer += Time.deltaTime;
            MiniGameUIController.SyncTimer(_timer);
            yield return null;
        }
    }

    public override void Dispose ()
    {
        if (UIController != null)
            RemoveUIListeners();
        
        _timerCoroutine.Dispose();
        _stopwatchEntryValues.Clear();
        _stopwatchEntryViews.DisposeAndClear();
        
        base.Dispose();
    }
}