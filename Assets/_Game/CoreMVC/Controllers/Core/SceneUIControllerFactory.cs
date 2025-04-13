using VContainer;
using VContainer.Unity;

public static class SceneUIControllerFactory
{
    public static SceneUIController CreateScope (
        out LifetimeScope uiControllerScope,
        LifetimeScope parentScope
    )
    {
        SceneUIControllerInstaller installer = new();
        uiControllerScope = parentScope.CreateChild(installer, "UIControllerScope");
        
        return uiControllerScope.Container.Resolve<SceneUIController>();
    }
}