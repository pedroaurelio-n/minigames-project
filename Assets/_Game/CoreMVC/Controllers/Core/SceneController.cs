using System;

public class SceneController : IDisposable
{
    // public MouseInputController MouseInputController { get; }
    public TouchInputController TouchInputController { get; }
    public MiniGameSceneChangerController MiniGameSceneChangerController { get; }
    
    public ButtonMashMiniGameController ButtonMashMiniGameController { get; }
    public ButtonStopwatchMiniGameController ButtonStopwatchMiniGameController { get; }
    public DragSortMiniGameController DragSortMiniGameController { get; }
    public DragRemoveMiniGameController DragRemoveMiniGameController { get; }
    public JoystickRotateMiniGameController JoystickRotateMiniGameController { get; }
    public JoystickAimMiniGameController JoystickAimMiniGameController { get; }
    public LongPressBombsMiniGameController LongPressBombsMiniGameController { get; }
    public MoveFindMiniGameController MoveFindMiniGameController { get; }
    public SwipeThrowMiniGameController SwipeThrowMiniGameController { get; }
    public TapDestroyMiniGameController TapDestroyMiniGameController { get; }
    public TapFloatingMiniGameController TapFloatingMiniGameController { get; }
    public TapMovingMiniGameController TapMovingMiniGameController { get; }

    public SceneController (
        // MouseInputController mouseInputController,
        TouchInputController touchInputController,
        MiniGameSceneChangerController miniGameSceneChangerController,
        ButtonMashMiniGameController buttonMashMiniGameController,
        ButtonStopwatchMiniGameController buttonStopwatchMiniGameController,
        DragSortMiniGameController dragSortMiniGameController,
        DragRemoveMiniGameController dragRemoveMiniGameController,
        LongPressBombsMiniGameController longPressBombsMiniGameController,
        JoystickRotateMiniGameController joystickRotateMiniGameController,
        JoystickAimMiniGameController joystickAimMiniGameController,
        MoveFindMiniGameController moveFindMiniGameController,
        SwipeThrowMiniGameController swipeThrowMiniGameController,
        TapDestroyMiniGameController tapDestroyMiniGameController,
        TapFloatingMiniGameController tapFloatingMiniGameController,
        TapMovingMiniGameController tapMovingMiniGameController
    )
    {
        // MouseInputController = mouseInputController;
        TouchInputController = touchInputController;
        MiniGameSceneChangerController = miniGameSceneChangerController;
        
        ButtonMashMiniGameController = buttonMashMiniGameController;
        ButtonStopwatchMiniGameController = buttonStopwatchMiniGameController;
        DragSortMiniGameController = dragSortMiniGameController;
        DragRemoveMiniGameController = dragRemoveMiniGameController;
        JoystickRotateMiniGameController = joystickRotateMiniGameController;
        JoystickAimMiniGameController = joystickAimMiniGameController;
        LongPressBombsMiniGameController = longPressBombsMiniGameController;
        MoveFindMiniGameController = moveFindMiniGameController;
        SwipeThrowMiniGameController = swipeThrowMiniGameController;
        TapDestroyMiniGameController = tapDestroyMiniGameController;
        TapFloatingMiniGameController = tapFloatingMiniGameController;
        TapMovingMiniGameController = tapMovingMiniGameController;
    }

    public void Initialize ()
    {
        // MouseInputController.Initialize();
        TouchInputController.Initialize();
        MiniGameSceneChangerController.Initialize();
        
        ButtonMashMiniGameController.Initialize();
        ButtonStopwatchMiniGameController.Initialize();
        DragSortMiniGameController.Initialize();
        DragRemoveMiniGameController.Initialize();
        JoystickRotateMiniGameController.Initialize();
        JoystickAimMiniGameController.Initialize();
        LongPressBombsMiniGameController.Initialize();
        MoveFindMiniGameController.Initialize();
        SwipeThrowMiniGameController.Initialize();
        TapDestroyMiniGameController.Initialize();
        TapFloatingMiniGameController.Initialize();
        TapMovingMiniGameController.Initialize();
    }

    public void LateInitialize ()
    {
    }

    public void Dispose() { }
}