using System;

public class GameController : IDisposable
{
    // public MouseInputController MouseInputController { get; }
    public TouchInputController TouchInputController { get; }
    public SceneChangerUIController SceneChangerUIController { get; }
    
    public TapObjectsMiniGameController TapObjectsMiniGameController { get; }
    public ThrowObjectsMiniGameController ThrowObjectsMiniGameController { get; }

    public GameController (
        // MouseInputController mouseInputController,
        TouchInputController touchInputController,
        SceneChangerUIController sceneChangerUIController,
        TapObjectsMiniGameController tapObjectsMiniGameController,
        ThrowObjectsMiniGameController throwObjectsMiniGameController
    )
    {
        // MouseInputController = mouseInputController;
        TouchInputController = touchInputController;
        SceneChangerUIController = sceneChangerUIController;
        TapObjectsMiniGameController = tapObjectsMiniGameController;
        ThrowObjectsMiniGameController = throwObjectsMiniGameController;
    }

    public void Initialize ()
    {
        // MouseInputController.Initialize();
        TouchInputController.Initialize();
        SceneChangerUIController.Initialize();
        TapObjectsMiniGameController.Initialize();
        ThrowObjectsMiniGameController.Initialize();
    }

    public void LateInitialize ()
    {
    }

    public void Dispose () { }
}