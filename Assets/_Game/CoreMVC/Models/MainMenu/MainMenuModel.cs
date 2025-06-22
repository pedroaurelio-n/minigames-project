using System;

public class MainMenuModel : IMainMenuModel
{
    public event Action<MainMenuState> OnMainMenuStateChanged;
    
    public int HighScore => _data.HighScore;

    readonly MiniGameData _data;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    readonly IPersistenceModel _persistenceModel;
    
    public MainMenuModel (
        MiniGameData data,
        IMenuSceneChangerModel menuSceneChangerModel,
        IPersistenceModel persistenceModel
    )
    {
        _data = data;
        _menuSceneChangerModel = menuSceneChangerModel;
        _persistenceModel = persistenceModel;
    }

    public void ChangeMainMenuState (MainMenuState state) => OnMainMenuStateChanged?.Invoke(state);

    public void PlayGame () => _menuSceneChangerModel.ChangeToNewMiniGame();

    public void SelectLevel (int index) => _menuSceneChangerModel.ChangeToDesiredMiniGame(index);

    public void ClearSave ()
    {
        _persistenceModel.ClearSave();
        
        //TODO pedro: check this SceneChangerModel inheritance/structure
        _menuSceneChangerModel.ReloadGame();
    }
}