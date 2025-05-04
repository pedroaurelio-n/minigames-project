using VContainer.Unity;
using UnityEngine;

public class MenuCoreFactory
{
    public static void CreateScope (
        out MenuView mainMenuView,
        out MenuUIView mainMenuUIView,
        out LifetimeScope menuScope,
        LifetimeScope parentScope,
        FadeToBlackManager fadeToBlackManager,
        PoolableViewFactory poolableViewFactory,
        IGameSessionInfoProvider gameSessionInfoProvider,
        ILoadingManager loadingManager,
        IPlayerInfoModel playerInfoModel,
        SettingsManager settingsManager,
        IRandomProvider randomProvider,
        IPhysicsProvider physicsProvider,
        ICameraProvider cameraProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        mainMenuView = Object.Instantiate(Resources.Load<MenuView>($"{gameSessionInfoProvider.CurrentScene}View"));
        mainMenuUIView =
            Object.Instantiate(Resources.Load<MenuUIView>($"{gameSessionInfoProvider.CurrentScene}UIView"));
        
        MenuCoreInstaller installer = new(
            mainMenuView,
            mainMenuUIView,
            poolableViewFactory,
            fadeToBlackManager,
            gameSessionInfoProvider,
            loadingManager,
            playerInfoModel,
            settingsManager,
            randomProvider,
            physicsProvider,
            cameraProvider,
            coroutineRunner
        );
        menuScope = parentScope.CreateChild(installer, "MenuScope");
    }
}