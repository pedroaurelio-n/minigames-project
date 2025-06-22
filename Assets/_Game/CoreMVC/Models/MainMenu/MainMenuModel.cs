using System;

public class MainMenuModel : IMainMenuModel
{
    public event Action<MainMenuState> OnMainMenuStateChanged;
    
    public int HighScore => _data.HighScore;

    readonly MiniGameData _data;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    
    public MainMenuModel (
        MiniGameData data,
        IMenuSceneChangerModel menuSceneChangerModel
    )
    {
        _data = data;
        _menuSceneChangerModel = menuSceneChangerModel;
    }

    public void ChangeMainMenuState (MainMenuState state)
    {
        OnMainMenuStateChanged?.Invoke(state);
    }

    public void PlayGame ()
    {
        _menuSceneChangerModel.ChangeToNewMiniGame();
    }

    public void SelectLevel (int index)
    {
        _menuSceneChangerModel.ChangeToDesiredMiniGame(index);
    }
}