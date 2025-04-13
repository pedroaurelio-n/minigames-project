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
        PoolableViewFactory poolableViewFactory,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        //TODO pedro: handle this when non-minigame scenes exists
        sceneView = Object.Instantiate(Resources.Load<SceneView>($"{gameSessionInfoProvider.CurrentScene}View"));
        sceneView.Initialize();
        
        SceneViewInstaller viewInstaller = new(sceneView, poolableViewFactory);
        viewScope = parentScope.CreateChild(viewInstaller);
        viewScope.name = "ViewScope";

        //TODO pedro: don't recreate persistent ui view
        sceneUIView = Object.Instantiate(Resources.Load<SceneUIView>("SceneUIView"));
        SceneUIViewInstaller uiViewInstaller = new(sceneUIView);
        uiViewScope = viewScope.CreateChild(uiViewInstaller);
        uiViewScope.name = "UIViewScope";
    }
}