public class MainMenuModel : IMainMenuModel
{
    const string FIRST_MINIGAME = "MiniGame1";

    public int HighScore => _playerInfoModel.HighScore;

    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    
    public MainMenuModel (
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _playerInfoModel = playerInfoModel;
        _menuSceneChangerModel = menuSceneChangerModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }

    public void PlayGame ()
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = true;
        _menuSceneChangerModel.ChangeScene(FIRST_MINIGAME);
    }
}