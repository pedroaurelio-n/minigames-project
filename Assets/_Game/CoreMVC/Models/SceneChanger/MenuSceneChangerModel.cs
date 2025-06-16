public class MenuSceneChangerModel : BaseSceneChangerModel, IMenuSceneChangerModel
{
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly IRandomProvider _randomProvider;
    readonly IMiniGameSystemSettings _miniGameSystemSettings;
    readonly IMiniGameSettingsAccessor _miniGameSettingsAccessor;
    
    public MenuSceneChangerModel (
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        IRandomProvider randomProvider,
        IMiniGameSystemSettings miniGameSystemSettings,
        IMiniGameSettingsAccessor miniGameSettingsAccessor
    ) : base(loadingManager)
    {
        _playerInfoModel = playerInfoModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _randomProvider = randomProvider;
        _miniGameSystemSettings = miniGameSystemSettings;
        _miniGameSettingsAccessor = miniGameSettingsAccessor;
    }

    public void ChangeToNewMiniGame ()
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = true;
        
        MiniGameType randomType = _randomProvider.PickRandom(_miniGameSystemSettings.ActiveMiniGames);
        _gameSessionInfoProvider.CurrentMiniGameType = randomType;
        string stringId =
            _miniGameSettingsAccessor.GetSettingsByType(_gameSessionInfoProvider.CurrentMiniGameType).StringId;
        ChangeScene($"{SceneManagerUtils.MiniGameScenePrefix}{stringId}");
    }

    public void ChangeToDesiredMiniGame (int index)
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = false;
        
        _gameSessionInfoProvider.CurrentMiniGameType = (MiniGameType)index;
        ChangeScene($"{SceneManagerUtils.MiniGameScenePrefix}{_gameSessionInfoProvider.CurrentMiniGameType}");
    }
    
    public void ChangeToMainMenu ()
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = false;
        
        ChangeScene(SceneManagerUtils.MainMenuSceneName);
    }
}