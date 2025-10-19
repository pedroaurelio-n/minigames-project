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
    public TapFloatingMiniGameController TapFloatingMiniGameController { get; }
    public TapMovingMiniGameController TapMovingMiniGameController { get; }
    public ButtonStopwatchMiniGameController ButtonStopwatchMiniGameController { get; }

    public SceneController (
        // MouseInputController mouseInputController,
        TouchInputController touchInputController,
        MiniGameSceneChangerController miniGameSceneChangerController,
        TapDestroyMiniGameController tapDestroyMiniGameController,
        SwipeThrowMiniGameController swipeThrowMiniGameController,
        DragSortMiniGameController dragSortMiniGameController,
        MoveFindMiniGameController moveFindMiniGameController,
        ButtonMashMiniGameController buttonMashMiniGameController,
        JoystickRotateMiniGameController joystickRotateMiniGameController,
        TapFloatingMiniGameController tapFloatingMiniGameController,
        TapMovingMiniGameController tapMovingMiniGameController,
        ButtonStopwatchMiniGameController buttonStopwatchMiniGameController
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
        TapFloatingMiniGameController = tapFloatingMiniGameController;
        TapMovingMiniGameController = tapMovingMiniGameController;
        ButtonStopwatchMiniGameController = buttonStopwatchMiniGameController;
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
        TapFloatingMiniGameController.Initialize();
        TapMovingMiniGameController.Initialize();
        ButtonStopwatchMiniGameController.Initialize();
    }

    public void LateInitialize ()
    {
    }

    public void Dispose() { }
}