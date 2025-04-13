using UnityEngine.SceneManagement;

public class MiniGameSceneChangerModel : BaseSceneChangerModel, IMiniGameSceneChangerModel
{
    const string MINI_GAME_SCENE_PREFIX = "MiniGame";
    
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    public MiniGameSceneChangerModel (
        ILoadingManager loadingManager,
        IGameSessionInfoProvider gameSessionInfoProvider
    ) : base(loadingManager)
    {
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }
    
    public void ChangeToNewMiniGame (MiniGameType type)
    {
        int newSceneIndex = (int)type;
        string newSceneName = $"{MINI_GAME_SCENE_PREFIX}{newSceneIndex}";
        ChangeScene(newSceneName);
    }

    public void ChangeToNextMiniGame ()
    {
        int newSceneIndex = _gameSessionInfoProvider.CurrentSceneIndex + 1;
        if (newSceneIndex > SceneManager.sceneCountInBuildSettings)
            newSceneIndex = 1;
        
        string newSceneName = SceneManagerUtils.GetSceneNameFromBuildIndex(newSceneIndex);
        ChangeScene(newSceneName);
    }
}