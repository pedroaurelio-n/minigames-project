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

        builder.Register<TapDestroyMiniGameController>(Lifetime.Singleton);
        builder.Register<SwipeThrowMiniGameController>(Lifetime.Singleton);
        builder.Register<DragSortMiniGameController>(Lifetime.Singleton);
        builder.Register<MoveFindMiniGameController>(Lifetime.Singleton);
        builder.Register<ButtonMashMiniGameController>(Lifetime.Singleton);
        builder.Register<JoystickRotateMiniGameController>(Lifetime.Singleton);
        builder.Register<TapFloatingMiniGameController>(Lifetime.Singleton);
        builder.Register<TapMovingMiniGameController>(Lifetime.Singleton);
        
        builder.Register<SceneController>(Lifetime.Singleton);
    }
}