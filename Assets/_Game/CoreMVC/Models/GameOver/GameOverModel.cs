public class GameOverModel : IGameOverModel
{
    const string FIRST_MINIGAME = "MiniGame1";

    public int Lives => _playerInfoModel.CurrentLives;
    public int Score => _playerInfoModel.CurrentScore;
    public int HighScore => _playerInfoModel.HighScore;

    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    
    public GameOverModel (
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _playerInfoModel = playerInfoModel;
        _menuSceneChangerModel = menuSceneChangerModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }

    public void RestartGame ()
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = true;
        _menuSceneChangerModel.ChangeScene(FIRST_MINIGAME);
    }

    public void ReturnToMenu ()
    {
        _menuSceneChangerModel.ChangeScene("MainMenu");
    }
}