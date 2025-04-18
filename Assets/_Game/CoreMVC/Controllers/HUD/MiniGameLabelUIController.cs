using System;
using System.Collections;
using UnityEngine;

public class MiniGameLabelUIController : IDisposable
{
    const float DISABLE_DELAY = 2.5f;
    
    readonly IMiniGameManagerModel _miniGameManagerModel;
    readonly MiniGameLabelUIView _view;
    readonly UniqueCoroutine _instructionsCoroutine;
    readonly WaitForSeconds _waitForDelay;

    public MiniGameLabelUIController (
        IMiniGameManagerModel miniGameManagerModel,
        MiniGameLabelUIView view,
        ICoroutineRunner coroutineRunner
    )
    {
        _miniGameManagerModel = miniGameManagerModel;
        _view = view;
        _instructionsCoroutine = new UniqueCoroutine(coroutineRunner);
        _waitForDelay = new WaitForSeconds(DISABLE_DELAY);
    }

    public void Initialize ()
    {
        if (_miniGameManagerModel.ActiveMiniGame == null)
            return;
        
        AddListeners();
        SyncView();
    }

    void SyncView ()
    {
        _view.SetActive(true);
        _view.SetText(_miniGameManagerModel.ActiveMiniGame.Instructions, Color.white);
        _instructionsCoroutine.Start(InstructionsCoroutine());
    }

    IEnumerator InstructionsCoroutine ()
    {
        yield return _waitForDelay;
        _view.SetActive(false);
    }

    void AddListeners ()
    {
        _miniGameManagerModel.ActiveMiniGame.OnMiniGameEnded += HandleMiniGameEnded;
    }
    
    void RemoveListeners ()
    {
        _miniGameManagerModel.ActiveMiniGame.OnMiniGameEnded -= HandleMiniGameEnded;
    }

    void HandleMiniGameEnded (bool hasCompleted)
    {
        if (_instructionsCoroutine.IsRunning)
            _instructionsCoroutine.Stop();

        string message = hasCompleted ? $"You win!" : $"You lose!";
        Color color = hasCompleted ? Color.green : Color.red;
        _view.SetActive(true);
        _view.SetText(message, color);
    }

    public void Dispose ()
    {
        if (_miniGameManagerModel.ActiveMiniGame == null)
            return;
        _instructionsCoroutine.Dispose();
        RemoveListeners();
    }
}