using VContainer;
using VContainer.Unity;

public static class SceneControllerFactory
{
    public static SceneController CreateScope (
        out LifetimeScope controllerScope,
        LifetimeScope parentScope
    )
    {
        SceneControllerInstaller installer = new();
        controllerScope = parentScope.CreateChild(installer, "ControllerScope");
        
        return controllerScope.Container.Resolve<SceneController>();
    }
}