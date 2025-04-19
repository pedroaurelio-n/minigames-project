using System;
using System.Collections;
using UnityEngine;

public class MiniGameManagerModel : IMiniGameManagerModel
{
    const float CHANGE_DELAY = 1.5f;
    
    public event Action OnMiniGameChange;
    
    public IMiniGameModel ActiveMiniGame => _activeMiniGame;
    public MiniGameType ActiveMiniGameType => _activeMiniGame.Type;

    IMiniGameModel _activeMiniGame;

    readonly IMiniGameSelectorModel _miniGameSelectorModel;
    readonly IMiniGameModelFactory _miniGameModelFactory;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly UniqueCoroutine _changeCoroutine;
    readonly WaitForSeconds _waitForChange;

    public MiniGameManagerModel (
        IMiniGameSelectorModel miniGameSelectorModel,
        IMiniGameModelFactory miniGameModelFactory,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        _miniGameSelectorModel = miniGameSelectorModel;
        _miniGameModelFactory = miniGameModelFactory;
        _playerInfoModel = playerInfoModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _changeCoroutine = new UniqueCoroutine(coroutineRunner);
        _waitForChange = new WaitForSeconds(CHANGE_DELAY);
    }

    public void Initialize ()
    {
        //TODO pedro: handle this when other non-minigame scenes exists
        // if (_gameSessionInfoProvider.CurrentSceneIndex > _miniGameSelectorModel.AllTypes.Count)
        //     return;
        
        MiniGameType chosenType = (MiniGameType)_gameSessionInfoProvider.CurrentSceneIndex;
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
        OnMiniGameChange?.Invoke();
    }

    public void Dispose()
    {
        _changeCoroutine.Dispose();
        RemoveListeners(_activeMiniGame);
        _activeMiniGame?.Dispose();
        _activeMiniGame = null;
    }
}