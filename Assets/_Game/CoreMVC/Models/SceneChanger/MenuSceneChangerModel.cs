using System.Linq;

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
    ) : base(loadingManager, gameSessionInfoProvider)
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

        IMiniGameSkillTierSettings firstTierActiveMiniGames =
            _miniGameSystemSettings.PoolSettings.SkillTierSettings.OrderBy(x => x.Tier).First();
        
        MiniGameType randomType = _randomProvider.PickRandom(firstTierActiveMiniGames.ActiveMiniGames);
        _gameSessionInfoProvider.CurrentMiniGameType = randomType;
        
        ChangeToMinigameScene();
    }

    public void ChangeToDesiredMiniGame (int index)
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = false;
        _gameSessionInfoProvider.CurrentMiniGameType = (MiniGameType)index;
        
        ChangeToMinigameScene();
    }
    
    public void ChangeToMainMenu ()
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = false;
        
        ChangeScene(SceneManagerUtils.MainMenuSceneName);
    }
    
    void ChangeToMinigameScene ()
    {
        IMiniGameSettings miniGameSettings = _miniGameSettingsAccessor.GetSettingsFromCurrentMinigame();
        string sceneViewName = $"{SceneManagerUtils.MiniGameScenePrefix}{miniGameSettings.StringId}";
        string sceneName = miniGameSettings.HasCustomScene
            ? sceneViewName
            : SceneManagerUtils.MiniGameDefaultSceneName;
        ChangeScene(sceneName, sceneViewName);
    }
}