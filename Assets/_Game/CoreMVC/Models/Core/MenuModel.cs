public class MenuModel : IMenuModel
{
    public IMainMenuModel MainMenuModel { get; }
    public IGameOverModel GameOverModel { get; }

    public MenuModel (
        IMainMenuModel mainMenuModel,
        IGameOverModel gameOverModel
    )
    {
        MainMenuModel = mainMenuModel;
        GameOverModel = gameOverModel;
    }

    public void Initialize ()
    {
    }

    public void LateInitialize ()
    {
    }
    
    public void Dispose () { }
}