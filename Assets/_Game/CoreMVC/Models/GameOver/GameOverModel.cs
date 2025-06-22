public class GameOverModel : IGameOverModel
{
    public int Lives => _playerInfoModel.CurrentLives;
    public int Score => _playerInfoModel.CurrentScore;
    public int HighScore => _data.HighScore;

    readonly IPersistenceModel _persistenceModel;
    readonly MiniGameData _data;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    
    public GameOverModel (
        IPersistenceModel persistenceModel,
        MiniGameData data,
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel
    )
    {
        _persistenceModel = persistenceModel;
        _data = data;
        _playerInfoModel = playerInfoModel;
        _menuSceneChangerModel = menuSceneChangerModel;
    }

    public void RegisterHighScore ()
    {
        //TODO pedro: create separate IPersistenceModel helper to manage flushing (prevent Data from being accessible from the same flushing class)
        _data.HighScore = HighScore;
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