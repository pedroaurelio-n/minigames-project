using UnityEngine.SceneManagement;

public class MiniGameSceneChangerModel : BaseSceneChangerModel, IMiniGameSceneChangerModel
{
    const string MINI_GAME_SCENE_PREFIX = "MiniGame";
    
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly IPlayerInfoModel _playerInfoModel;

    public MiniGameSceneChangerModel (
        ILoadingManager loadingManager,
        IGameSessionInfoProvider gameSessionInfoProvider,
        IPlayerInfoModel playerInfoModel
    ) : base(loadingManager)
    {
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _playerInfoModel = playerInfoModel;
    }
    
    public void ChangeToNewMiniGame (MiniGameType type)
    {
        if (!_playerInfoModel.HasLivesRemaining)
        {
            string gameOverScene = $"GameOver";
            ChangeScene(gameOverScene);
            return;
        }
        
        int newSceneIndex = (int)type;
        string newSceneName = $"{MINI_GAME_SCENE_PREFIX}{newSceneIndex}";
        ChangeScene(newSceneName);
    }

    public void ChangeToNextMiniGame ()
    {
        int newSceneIndex = (int)_gameSessionInfoProvider.CurrentMiniGameType + 1 + 1;
        if (newSceneIndex > SceneManager.sceneCountInBuildSettings)
            newSceneIndex = 1;
        
        string newSceneName = SceneManagerUtils.GetSceneNameFromBuildIndex(newSceneIndex);
        ChangeScene(newSceneName);
    }
}