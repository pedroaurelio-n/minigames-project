using System;

public class GameController : IDisposable
{
    // public MouseInputController MouseInputController { get; }
    public TouchInputController TouchInputController { get; }
    public MiniGameSceneChangerController MiniGameSceneChangerController { get; }
    
    public TapObjectsMiniGameController TapObjectsMiniGameController { get; }
    public ThrowObjectsMiniGameController ThrowObjectsMiniGameController { get; }
    public DragObjectsMiniGameController DragObjectsMiniGameController { get; }
    public FindObjectMiniGameController FindObjectMiniGameController { get; }

    public GameController (
        // MouseInputController mouseInputController,
        TouchInputController touchInputController,
        MiniGameSceneChangerController miniGameSceneChangerController,
        TapObjectsMiniGameController tapObjectsMiniGameController,
        ThrowObjectsMiniGameController throwObjectsMiniGameController,
        DragObjectsMiniGameController dragObjectsMiniGameController,
        FindObjectMiniGameController findObjectMiniGameController
    )
    {
        // MouseInputController = mouseInputController;
        TouchInputController = touchInputController;
        MiniGameSceneChangerController = miniGameSceneChangerController;
        TapObjectsMiniGameController = tapObjectsMiniGameController;
        ThrowObjectsMiniGameController = throwObjectsMiniGameController;
        DragObjectsMiniGameController = dragObjectsMiniGameController;
        FindObjectMiniGameController = findObjectMiniGameController;
    }

    public void Initialize ()
    {
        // MouseInputController.Initialize();
        TouchInputController.Initialize();
        MiniGameSceneChangerController.Initialize();
        TapObjectsMiniGameController.Initialize();
        ThrowObjectsMiniGameController.Initialize();
        DragObjectsMiniGameController.Initialize();
        FindObjectMiniGameController.Initialize();
    }

    public void LateInitialize ()
    {
    }

    public void Dispose () { }
}