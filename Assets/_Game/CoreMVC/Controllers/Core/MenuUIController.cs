using System;

public class MenuUIController : IDisposable
{
    public MainMenuUIController MainMenuUIController { get; private set; }
    public GameOverUIController GameOverUIController { get; private set; }

    public MenuUIController (
        MainMenuUIController mainMenuUIController,
        GameOverUIController gameOverUIController
    )
    {
        MainMenuUIController = mainMenuUIController;
        GameOverUIController = gameOverUIController;
    }

    public void Initialize ()
    {
        MainMenuUIController.Initialize();
        GameOverUIController.Initialize();
    }
    
    public void LateInitialize ()
    {
    }
    
    public void Dispose () { }
}