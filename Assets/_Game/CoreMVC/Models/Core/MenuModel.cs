public class MenuModel : IMenuModel
{
    public IMainMenuModel MainMenuModel { get; }
    public IGameOverModel GameOverModel { get; }
    public IMiniGameDifficultyModel MiniGameDifficultyModel { get; }
    public IMiniGameSelectorModel MiniGameSelectorModel { get; }

    public MenuModel (
        IMainMenuModel mainMenuModel,
        IGameOverModel gameOverModel,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameSelectorModel miniGameSelectorModel
    )
    {
        MainMenuModel = mainMenuModel;
        GameOverModel = gameOverModel;
        MiniGameDifficultyModel = miniGameDifficultyModel;
        MiniGameSelectorModel = miniGameSelectorModel;
    }

    public void Initialize ()
    {
        MiniGameDifficultyModel.Initialize();
        MiniGameSelectorModel.Initialize();
    }

    public void LateInitialize ()
    {
    }
    
    public void Dispose () { }
}