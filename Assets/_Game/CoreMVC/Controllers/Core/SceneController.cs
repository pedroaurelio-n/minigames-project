using System;

public class SceneController : IDisposable
{
    // public MouseInputController MouseInputController { get; }
    public TouchInputController TouchInputController { get; }
    public MiniGameSceneChangerController MiniGameSceneChangerController { get; }
    
    public TapDestroyMiniGameController TapDestroyMiniGameController { get; }
    public SwipeThrowMiniGameController SwipeThrowMiniGameController { get; }
    public DragSortMiniGameController DragSortMiniGameController { get; }
    public MoveFindMiniGameController MoveFindMiniGameController { get; }
    public ButtonMashMiniGameController ButtonMashMiniGameController { get; }
    public JoystickRotateMiniGameController JoystickRotateMiniGameController { get; }

    public SceneController (
        // MouseInputController mouseInputController,
        TouchInputController touchInputController,
        MiniGameSceneChangerController miniGameSceneChangerController,
        TapDestroyMiniGameController tapDestroyMiniGameController,
        SwipeThrowMiniGameController swipeThrowMiniGameController,
        DragSortMiniGameController dragSortMiniGameController,
        MoveFindMiniGameController moveFindMiniGameController,
        ButtonMashMiniGameController buttonMashMiniGameController,
        JoystickRotateMiniGameController joystickRotateMiniGameController
    )
    {
        // MouseInputController = mouseInputController;
        TouchInputController = touchInputController;
        MiniGameSceneChangerController = miniGameSceneChangerController;
        TapDestroyMiniGameController = tapDestroyMiniGameController;
        SwipeThrowMiniGameController = swipeThrowMiniGameController;
        DragSortMiniGameController = dragSortMiniGameController;
        MoveFindMiniGameController = moveFindMiniGameController;
        ButtonMashMiniGameController = buttonMashMiniGameController;
        JoystickRotateMiniGameController = joystickRotateMiniGameController;
    }

    public void Initialize ()
    {
        // MouseInputController.Initialize();
        TouchInputController.Initialize();
        MiniGameSceneChangerController.Initialize();
        TapDestroyMiniGameController.Initialize();
        SwipeThrowMiniGameController.Initialize();
        DragSortMiniGameController.Initialize();
        MoveFindMiniGameController.Initialize();
        ButtonMashMiniGameController.Initialize();
        JoystickRotateMiniGameController.Initialize();
    }

    public void LateInitialize ()
    {
    }

    public void Dispose() { }
}