public class MiniGameManagerModel : IMiniGameManagerModel
{
    public IMiniGameModel ActiveMiniGame => _activeMiniGame;
    public MiniGameType ActiveMiniGameType => _activeMiniGame.Type;

    IMiniGameModel _activeMiniGame;

    readonly IMiniGameModelFactory _miniGameModelFactory;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    public MiniGameManagerModel (
        IMiniGameModelFactory miniGameModelFactory,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _miniGameModelFactory = miniGameModelFactory;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }

    public void Initialize ()
    {
        MiniGameType chosenType = (MiniGameType)(_gameSessionInfoProvider.CurrentSceneIndex - 1);
        _activeMiniGame = _miniGameModelFactory.CreateMiniGameBasedOnType(chosenType);
        //TODO pedro: remove null conditional
        _activeMiniGame?.Initialize();
    }

    public void LateInitialize ()
    {
        _activeMiniGame?.LateInitialize();
    }
}