using System;

public class SceneUIController : IDisposable
{
    SceneChangerUIController SceneChangerUIController { get; }
    MiniGameTimerUIController MiniGameTimerUIController { get; }
    MiniGameLabelUIController MiniGameLabelUIController { get; }
    
    public SceneUIController (
        SceneChangerUIController sceneChangerUIController,
        MiniGameTimerUIController miniGameTimerUIController,
        MiniGameLabelUIController miniGameLabelUIController
    )
    {
        SceneChangerUIController = sceneChangerUIController;
        MiniGameTimerUIController = miniGameTimerUIController;
        MiniGameLabelUIController = miniGameLabelUIController;
    }

    public void Initialize ()
    {
        SceneChangerUIController.Initialize();
        MiniGameTimerUIController.Initialize();
        MiniGameLabelUIController.Initialize();
    }

    public void LateInitialize ()
    {
    }
    
    public void Dispose() { }
}