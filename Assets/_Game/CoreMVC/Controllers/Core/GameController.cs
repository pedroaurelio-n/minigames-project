public class GameController
{
    public MouseInputController MouseInputController { get; private set; }
    public SceneChangerUIController SceneChangerUIController { get; private set; }

    public GameController (
        MouseInputController mouseInputController,
        SceneChangerUIController sceneChangerUIController
    )
    {
        MouseInputController = mouseInputController;
        SceneChangerUIController = sceneChangerUIController;
    }

    public void Initialize ()
    {
        MouseInputController.Initialize();
        SceneChangerUIController.Initialize();
    }
}