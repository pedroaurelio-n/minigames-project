public class MiniGameSceneChangerModel : BaseSceneChangerModel, IMiniGameSceneChangerModel
{
    readonly IPlayerInfoModel _playerInfoModel;

    public MiniGameSceneChangerModel (
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel
    ) : base(loadingManager)
    {
        _playerInfoModel = playerInfoModel;
    }
    
    public void ChangeToNewMiniGame (MiniGameType type)
    {
        if (!_playerInfoModel.HasLivesRemaining)
        {
            ChangeScene(SceneManagerUtils.GameOverSceneName);
            return;
        }
        
        int newSceneIndex = (int)type;
        string newSceneName = $"{SceneManagerUtils.MiniGameScenePrefix}{newSceneIndex}";
        ChangeScene(newSceneName);
    }

    public void ChangeToMainMenu ()
    {
        ChangeScene(SceneManagerUtils.MainMenuSceneName);
    }
}