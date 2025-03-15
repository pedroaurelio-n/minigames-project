public class GameController
{
    // public MouseInputController MouseInputController { get; }
    public TouchInputController TouchInputController { get; }
    public SceneChangerUIController SceneChangerUIController { get; }

    public GameController (
        // MouseInputController mouseInputController,
        TouchInputController touchInputController,
        SceneChangerUIController sceneChangerUIController
    )
    {
        // MouseInputController = mouseInputController;
        TouchInputController = touchInputController;
        SceneChangerUIController = sceneChangerUIController;
    }

    public void Initialize ()
    {
        // MouseInputController.Initialize();
        TouchInputController.Initialize();
        SceneChangerUIController.Initialize();
    }
}