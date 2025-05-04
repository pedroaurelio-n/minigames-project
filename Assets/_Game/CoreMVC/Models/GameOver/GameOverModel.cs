public class GameOverModel : IGameOverModel
{
    public int Lives => _playerInfoModel.CurrentLives;
    public int Score => _playerInfoModel.CurrentScore;
    public int HighScore => _playerInfoModel.HighScore;

    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    
    public GameOverModel (
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel
    )
    {
        _playerInfoModel = playerInfoModel;
        _menuSceneChangerModel = menuSceneChangerModel;
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