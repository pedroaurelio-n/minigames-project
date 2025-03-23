using UnityEngine.SceneManagement;

public static class SceneManagerUtils
{
    public static string GetSceneNameFromBuildIndex (int index)
    {
        //TODO pedro: this is horrible
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
    }
}