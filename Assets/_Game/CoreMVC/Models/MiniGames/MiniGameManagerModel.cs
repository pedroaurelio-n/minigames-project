using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManagerModel : IMiniGameManagerModel
{
    public event Action OnMiniGameChanged;
    public event Action OnSingleMiniGameEnded;
    
    public IMiniGameModel ActiveMiniGame => _activeMiniGame;
    public MiniGameType ActiveMiniGameType => _activeMiniGame.Type;

    IMiniGameModel _activeMiniGame;

    readonly MiniGameData _data;
    readonly IMiniGameModelFactory _miniGameModelFactory;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly UniqueCoroutine _changeCoroutine;
    readonly WaitForSeconds _waitForChange;

    public MiniGameManagerModel (
        MiniGameData data,
        IMiniGameSystemSettings miniGameSystemSettings,
        IMiniGameModelFactory miniGameModelFactory,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        _data = data;
        _miniGameModelFactory = miniGameModelFactory;
        _playerInfoModel = playerInfoModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _changeCoroutine = new UniqueCoroutine(coroutineRunner);
        _waitForChange = new WaitForSeconds(miniGameSystemSettings.NextMiniGameDelay);
    }

    public void Initialize ()
    {
        MiniGameType chosenType = _gameSessionInfoProvider.CurrentMiniGameType;
        _activeMiniGame = _miniGameModelFactory.CreateMiniGameBasedOnType(chosenType);
        _activeMiniGame.Initialize();
        
        _gameSessionInfoProvider.CurrentMiniGameType = ActiveMiniGameType;
    }

    public void LateInitialize ()
    {
        _activeMiniGame.LateInitialize();
        AddMiniGameListeners();
    }

    public void ForceCompleteMiniGame () => _activeMiniGame.Complete();

    public void ForceFailMiniGame () => _activeMiniGame.ForceFailure();

    void AddMiniGameListeners()
    {
        _activeMiniGame.OnMiniGameEnded += HandleMiniGameEnded;
    }

    void RemoveMiniGameListeners ()
    {
        _activeMiniGame.OnMiniGameEnded -= HandleMiniGameEnded;
    }
    
    void HandleMiniGameEnded (bool hasCompleted)
    {
        _changeCoroutine.Start(ChangeCoroutine());
        ModifyMiniGameData(hasCompleted);
        
        if (hasCompleted)
            _playerInfoModel.ModifyScore(1);
        else
            _playerInfoModel.ModifyLives(-1);
    }

    void ModifyMiniGameData (bool hasCompleted)
    {
        if (!_gameSessionInfoProvider.HasStartedGameRun)
            return;

        Dictionary<string, int> chosenDict = hasCompleted ? _data.VictoriesInMiniGame : _data.DefeatsInMiniGame;
        chosenDict.TryAdd(_activeMiniGame.StringId, 0);
        chosenDict[_activeMiniGame.StringId]++;
    }

    void ModifyDefeatsData ()
    {
        if (!_gameSessionInfoProvider.HasStartedGameRun)
            return;
        
        _data.DefeatsInMiniGame.TryAdd(_activeMiniGame.StringId, 0);
        _data.DefeatsInMiniGame[_activeMiniGame.StringId]++;
    }

    IEnumerator ChangeCoroutine ()
    {
        yield return _waitForChange;
        
        if (_gameSessionInfoProvider.HasStartedGameRun)
            OnMiniGameChanged?.Invoke();
        else
            OnSingleMiniGameEnded?.Invoke();
    }

    public void Dispose()
    {
        _changeCoroutine.Dispose();
        RemoveMiniGameListeners();
        _activeMiniGame?.Dispose();
        _activeMiniGame = null;
    }
}