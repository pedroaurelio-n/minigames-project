using System;

public class MenuController : IDisposable
{
    public MainMenuController MainMenuController { get; private set; }
    public GameOverController GameOverController { get; private set; }

    public MenuController (
        MainMenuController mainMenuController,
        GameOverController gameOverController
    )
    {
        MainMenuController = mainMenuController;
        GameOverController = gameOverController;
    }

    public void Initialize ()
    {
    }
    
    public void LateInitialize ()
    {
    }
    
    public void Dispose () { }
}