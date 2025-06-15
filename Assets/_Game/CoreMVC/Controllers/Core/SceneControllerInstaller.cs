using VContainer;
using VContainer.Unity;

public class SceneControllerInstaller : IInstaller
{
    public void Install (IContainerBuilder builder)
    {
        //TODO pedro: delete mouse input classes
        // builder.Register<MouseInputController>(Lifetime.Singleton);
        builder.Register<TouchInputController>(Lifetime.Singleton);
        
        builder.Register<MiniGameSceneChangerController>(Lifetime.Singleton);

        builder.Register<TapObjectsMiniGameController>(Lifetime.Singleton);
        builder.Register<ThrowObjectsMiniGameController>(Lifetime.Singleton);
        builder.Register<DragObjectsMiniGameController>(Lifetime.Singleton);
        builder.Register<FindObjectMiniGameController>(Lifetime.Singleton);
        builder.Register<ClickMilestoneMiniGameController>(Lifetime.Singleton);
        builder.Register<JoystickRotateMiniGameController>(Lifetime.Singleton);
        
        builder.Register<SceneController>(Lifetime.Singleton);
    }
}