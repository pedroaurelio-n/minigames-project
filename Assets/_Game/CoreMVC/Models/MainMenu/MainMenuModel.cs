using System;

public class MainMenuModel : IMainMenuModel
{
    public event Action<MainMenuState> OnMainMenuStateChanged;
    
    public int HighScore => _playerInfoModel.HighScore;

    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    
    public MainMenuModel (
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel
    )
    {
        _playerInfoModel = playerInfoModel;
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