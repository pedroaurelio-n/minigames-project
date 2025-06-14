using System;

public class SceneController : IDisposable
{
    // public MouseInputController MouseInputController { get; }
    public TouchInputController TouchInputController { get; }
    public MiniGameSceneChangerController MiniGameSceneChangerController { get; }
    
    public TapObjectsMiniGameController TapObjectsMiniGameController { get; }
    public ThrowObjectsMiniGameController ThrowObjectsMiniGameController { get; }
    public DragObjectsMiniGameController DragObjectsMiniGameController { get; }
    public FindObjectMiniGameController FindObjectMiniGameController { get; }
    public ClickMilestoneMiniGameController ClickMilestoneMiniGameController { get; }
    public JoystickRotateMiniGameController JoystickRotateMiniGameController { get; }

    public SceneController (
        // MouseInputController mouseInputController,
        TouchInputController touchInputController,
        MiniGameSceneChangerController miniGameSceneChangerController,
        TapObjectsMiniGameController tapObjectsMiniGameController,
        ThrowObjectsMiniGameController throwObjectsMiniGameController,
        DragObjectsMiniGameController dragObjectsMiniGameController,
        FindObjectMiniGameController findObjectMiniGameController,
        ClickMilestoneMiniGameController clickMilestoneMiniGameController,
        JoystickRotateMiniGameController joystickRotateMiniGameController
    )
    {
        // MouseInputController = mouseInputController;
        TouchInputController = touchInputController;
        MiniGameSceneChangerController = miniGameSceneChangerController;
        TapObjectsMiniGameController = tapObjectsMiniGameController;
        ThrowObjectsMiniGameController = throwObjectsMiniGameController;
        DragObjectsMiniGameController = dragObjectsMiniGameController;
        FindObjectMiniGameController = findObjectMiniGameController;
        ClickMilestoneMiniGameController = clickMilestoneMiniGameController;
        JoystickRotateMiniGameController = joystickRotateMiniGameController;
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
        ClickMilestoneMiniGameController.Initialize();
        JoystickRotateMiniGameController.Initialize();
    }

    public void LateInitialize ()
    {
    }

    public void Dispose() { }
}