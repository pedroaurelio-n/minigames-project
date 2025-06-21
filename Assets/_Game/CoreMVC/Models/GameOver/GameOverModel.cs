public class GameOverModel : IGameOverModel
{
    public int Lives => _playerInfoModel.CurrentLives;
    public int Score => _playerInfoModel.CurrentScore;
    public int HighScore => _gameSessionData.HighScore;

    readonly IPersistenceModel _persistenceModel;
    readonly GameSessionData _gameSessionData;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    
    public GameOverModel (
        IPersistenceModel persistenceModel,
        GameSessionData gameSessionData,
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel
    )
    {
        _persistenceModel = persistenceModel;
        _gameSessionData = gameSessionData;
        _playerInfoModel = playerInfoModel;
        _menuSceneChangerModel = menuSceneChangerModel;
    }

    public void RegisterHighScore ()
    {
        //TODO pedro: create separate IPersistenceModel helper to manage flushing (prevent Data from being accessible from the same flushing class)
        _gameSessionData.HighScore = HighScore;
        _persistenceModel.Flush();
    }

    public void RestartGame ()
    {
        _menuSceneChangerModel.ChangeToNewMiniGame();
    }

    public void ReturnToMenu ()
    {
        _menuSceneChangerModel.ChangeToMainMenu();
    }
}