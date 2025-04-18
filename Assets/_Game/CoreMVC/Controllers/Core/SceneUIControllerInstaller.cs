using VContainer;
using VContainer.Unity;

public class SceneUIControllerInstaller : IInstaller
{
    public void Install (IContainerBuilder builder)
    {
        builder.Register<SceneChangerUIController>(Lifetime.Singleton);

        builder.Register<MiniGameLabelUIController>(Lifetime.Singleton);
        builder.Register<MiniGameTimerUIController>(Lifetime.Singleton);
        
        builder.Register<SceneUIController>(Lifetime.Singleton);
    }
}