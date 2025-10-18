public class MiniGameSceneChangerModel : BaseSceneChangerModel, IMiniGameSceneChangerModel
{
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMiniGameSettingsAccessor _miniGameSettingsAccessor;

    public MiniGameSceneChangerModel (
        ILoadingManager loadingManager,
        IGameSessionInfoProvider gameSessionInfoProvider,
        IPlayerInfoModel playerInfoModel,
        IMiniGameSettingsAccessor miniGameSettingsAccessor
    ) : base(loadingManager, gameSessionInfoProvider)
    {
        _playerInfoModel = playerInfoModel;
        _miniGameSettingsAccessor = miniGameSettingsAccessor;
    }
    
    public void ChangeToNewMiniGame (MiniGameType type)
    {
        if (!_playerInfoModel.HasLivesRemaining)
        {
            ChangeScene(SceneManagerUtils.GameOverSceneName);
            return;
        }

        IMiniGameSettings miniGameSettings = _miniGameSettingsAccessor.GetSettingsByType(type);
        string sceneViewName = $"{SceneManagerUtils.MiniGameScenePrefix}{miniGameSettings.StringId}";
        string sceneName = miniGameSettings.HasCustomScene
            ? sceneViewName
            : SceneManagerUtils.MiniGameDefaultSceneName;
        ChangeScene(sceneName, sceneViewName);
    }

    public void ChangeToMainMenu ()
    {
        ChangeScene(SceneManagerUtils.MainMenuSceneName);
    }
}