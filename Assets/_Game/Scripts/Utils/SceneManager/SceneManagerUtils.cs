using UnityEngine.SceneManagement;

public static class SceneManagerUtils
{
    public static readonly string MainMenuSceneName = "MainMenu";
    public static readonly string GameOverSceneName = "GameOver";
    public static readonly string MiniGameScenePrefix = "MiniGame";
    
    public static string GetSceneNameFromBuildIndex (int index)
    {
        //TODO pedro: this is horrible
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
    }
}