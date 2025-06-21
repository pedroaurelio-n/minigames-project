using VContainer.Unity;
using UnityEngine;

public class MenuCoreFactory
{
    public static void CreateScope (
        out MenuView mainMenuView,
        out MenuUIView mainMenuUIView,
        out LifetimeScope menuScope,
        IPersistenceModel persistenceModel,
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
        IDateTimeProvider dateTimeProvider,
        ICoroutineRunner coroutineRunner
    )
    {
        mainMenuView = Object.Instantiate(Resources.Load<MenuView>($"{gameSessionInfoProvider.CurrentScene}View"));
        mainMenuUIView =
            Object.Instantiate(Resources.Load<MenuUIView>($"{gameSessionInfoProvider.CurrentScene}UIView"));
        
        MenuCoreInstaller installer = new(
            persistenceModel,
            persistenceModel.Data,
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
            dateTimeProvider,
            coroutineRunner
        );
        menuScope = parentScope.CreateChild(installer, "MenuScope");
    }
}