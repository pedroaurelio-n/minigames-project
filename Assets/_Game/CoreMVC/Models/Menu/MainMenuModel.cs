public class MainMenuModel : IMainMenuModel
{
    public int HighScore => _playerInfoModel.HighScore;

    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly IRandomProvider _randomProvider;
    
    public MainMenuModel (
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        IRandomProvider randomProvider
    )
    {
        _playerInfoModel = playerInfoModel;
        _menuSceneChangerModel = menuSceneChangerModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _randomProvider = randomProvider;
    }

    public void PlayGame ()
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = true;

        MiniGameType randomType = _randomProvider.RandomEnumValue<MiniGameType>();
        _gameSessionInfoProvider.CurrentMiniGameType = randomType;
        _menuSceneChangerModel.ChangeScene($"MiniGame{(int)randomType}");
    }
}