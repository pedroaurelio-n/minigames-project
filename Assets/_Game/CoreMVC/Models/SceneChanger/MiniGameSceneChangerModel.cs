public class MiniGameSceneChangerModel : BaseSceneChangerModel, IMiniGameSceneChangerModel
{
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMiniGameSettingsAccessor _miniGameSettingsAccessor;

    public MiniGameSceneChangerModel (
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        IMiniGameSettingsAccessor miniGameSettingsAccessor
    ) : base(loadingManager)
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
        
        string newStringId = _miniGameSettingsAccessor.GetSettingsByType(type).StringId;
        string newSceneName = $"{SceneManagerUtils.MiniGameScenePrefix}{newStringId}";
        ChangeScene(newSceneName);
    }

    public void ChangeToMainMenu ()
    {
        ChangeScene(SceneManagerUtils.MainMenuSceneName);
    }
}