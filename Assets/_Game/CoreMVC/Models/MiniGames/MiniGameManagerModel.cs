public class MiniGameManagerModel : IMiniGameManagerModel
{
    public IMiniGameModel ActiveMiniGame => _activeMiniGame;
    public MiniGameType ActiveMiniGameType => _activeMiniGame.Type;

    IMiniGameModel _activeMiniGame;

    readonly IMiniGameModelFactory _miniGameModelFactory;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    public MiniGameManagerModel (
        IMiniGameModelFactory miniGameModelFactory,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _miniGameModelFactory = miniGameModelFactory;
        _playerInfoModel = playerInfoModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }

    public void Initialize ()
    {
        MiniGameType chosenType = (MiniGameType)(_gameSessionInfoProvider.CurrentSceneIndex - 1);
        _activeMiniGame = _miniGameModelFactory.CreateMiniGameBasedOnType(chosenType);
        //TODO pedro: remove null conditional
        _activeMiniGame?.Initialize();
        AddMiniGameListeners(_activeMiniGame);
    }

    public void LateInitialize ()
    {
        _activeMiniGame?.LateInitialize();
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
        if (hasCompleted)
            _playerInfoModel.ModifyScore(1);
        else
            _playerInfoModel.ModifyLives(-1);
    }

    public void Dispose()
    {
        RemoveListeners(_activeMiniGame);
        _activeMiniGame?.Dispose();
        _activeMiniGame = null;
    }
}