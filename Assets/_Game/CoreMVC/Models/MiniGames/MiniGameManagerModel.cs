using System;
using System.Collections;
using UnityEngine;

public class MiniGameManagerModel : IMiniGameManagerModel
{
    public event Action OnMiniGameChanged;
    public event Action OnSingleMiniGameEnded;
    
    public IMiniGameModel ActiveMiniGame => _activeMiniGame;
    public MiniGameType ActiveMiniGameType => _activeMiniGame.Type;

    IMiniGameModel _activeMiniGame;

    readonly IMiniGameModelFactory _miniGameModelFactory;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly UniqueCoroutine _changeCoroutine;
    readonly WaitForSeconds _waitForChange;

    public MiniGameManagerModel (
        IMiniGameSystemSettings miniGameSystemSettings,
        IMiniGameModelFactory miniGameModelFactory,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        ICoroutineRunner coroutineRunner
    )
    {
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
        AddMiniGameListeners(_activeMiniGame);
    }

    void AddMiniGameListeners(IMiniGameModel activeMiniGame)
    {
        activeMiniGame.OnMiniGameEnded += HandleMiniGameEnded;
    }

    void RemoveListeners (IMiniGameModel activeMiniGame)
    {
        if (activeMiniGame == null)
            return;
        _activeMiniGame.OnMiniGameEnded -= HandleMiniGameEnded;
    }
    
    void HandleMiniGameEnded (bool hasCompleted)
    {
        _changeCoroutine.Start(ChangeCoroutine());
        
        if (hasCompleted)
            _playerInfoModel.ModifyScore(1);
        else
            _playerInfoModel.ModifyLives(-1);
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
        RemoveListeners(_activeMiniGame);
        _activeMiniGame?.Dispose();
        _activeMiniGame = null;
    }
}