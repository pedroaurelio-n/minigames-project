public class MenuSceneChangerModel : BaseSceneChangerModel, IMenuSceneChangerModel
{
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly IRandomProvider _randomProvider;
    readonly IMiniGameSystemSettings _miniGameSystemSettings;
    
    public MenuSceneChangerModel (
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        IRandomProvider randomProvider,
        IMiniGameSystemSettings miniGameSystemSettings
    ) : base(loadingManager)
    {
        _playerInfoModel = playerInfoModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _randomProvider = randomProvider;
        _miniGameSystemSettings = miniGameSystemSettings;
    }

    public void ChangeToNewMiniGame ()
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = true;
        
        MiniGameType randomType = _randomProvider.PickRandom(_miniGameSystemSettings.ActiveMiniGames);
        _gameSessionInfoProvider.CurrentMiniGameType = randomType;
        ChangeScene($"{SceneManagerUtils.MiniGameScenePrefix}{(int)randomType}");
    }

    public void ChangeToDesiredMiniGame (int index)
    {
        _playerInfoModel.Reset();
        _gameSessionInfoProvider.HasStartedGameRun = false;
        
        _gameSessionInfoProvider.CurrentMiniGameType = (MiniGameType)index;
        ChangeScene($"{SceneManagerUtils.MiniGameScenePrefix}{index}");
    }
}