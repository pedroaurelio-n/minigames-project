public class MainMenuModel : IMainMenuModel
{
    public int HighScore => _playerInfoModel.HighScore;

    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly IRandomProvider _randomProvider;
    readonly IMiniGameSystemSettings _miniGameSystemSettings;
    
    public MainMenuModel (
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        IRandomProvider randomProvider,
        IMiniGameSystemSettings miniGameSystemSettings
    )
    {
        _playerInfoModel = playerInfoModel;
        _menuSceneChangerModel = menuSceneChangerModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _randomProvider = randomProvider;
        _miniGameSystemSettings = miniGameSystemSettings;
    }

    public void PlayGame ()
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = true;

        MiniGameType randomType = _randomProvider.PickRandom(_miniGameSystemSettings.ActiveMiniGames);
        _gameSessionInfoProvider.CurrentMiniGameType = randomType;
        _menuSceneChangerModel.ChangeScene($"MiniGame{(int)randomType}");
    }
}