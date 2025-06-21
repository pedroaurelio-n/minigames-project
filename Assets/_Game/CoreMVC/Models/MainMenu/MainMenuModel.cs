using System;

public class MainMenuModel : IMainMenuModel
{
    public event Action<MainMenuState> OnMainMenuStateChanged;
    
    public int HighScore => _gameSessionData.HighScore;

    readonly GameSessionData _gameSessionData;
    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    
    public MainMenuModel (
        GameSessionData gameSessionData,
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel
    )
    {
        _gameSessionData = gameSessionData;
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