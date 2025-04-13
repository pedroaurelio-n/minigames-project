using VContainer;
using VContainer.Unity;

public class SceneUIControllerInstaller : IInstaller
{
    public void Install (IContainerBuilder builder)
    {
        builder.Register<SceneUIController>(Lifetime.Singleton);
    }
}