using UnityEngine;
using VContainer.Unity;

public static class SceneViewsFactory
{
    public static void CreateScopes (
        out SceneView sceneView,
        out SceneUIView sceneUIView,
        out LifetimeScope viewScope,
        out LifetimeScope uiViewScope,
        LifetimeScope parentScope,
        FadeToBlackManager fadeToBlackManager,
        PoolableViewFactory poolableViewFactory,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        sceneView = Object.Instantiate(Resources.Load<SceneView>($"{gameSessionInfoProvider.CurrentSceneViewName}SceneView"));
        sceneView.Initialize();
        
        SceneViewInstaller viewInstaller = new(sceneView, poolableViewFactory);
        viewScope = parentScope.CreateChild(viewInstaller, "ViewScope");

        //TODO pedro: don't recreate persistent ui view
        sceneUIView = Object.Instantiate(Resources.Load<SceneUIView>("SceneUIView"));
        SceneUIViewInstaller uiViewInstaller = new(sceneUIView, fadeToBlackManager);
        uiViewScope = viewScope.CreateChild(uiViewInstaller, "UIViewScope");
    }
}