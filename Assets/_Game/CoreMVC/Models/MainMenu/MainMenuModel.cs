using System;
using Firebase.Extensions;

public class MainMenuModel : IMainMenuModel
{
    public event Action<MainMenuState> OnMainMenuStateChanged;
    
    public int HighScore => _playerInfoModel.HighScore;
    public string User => _firebaseManager.IsAuthenticated ? _firebaseManager.CurrentUser.Email : null;

    readonly IPlayerInfoModel _playerInfoModel;
    readonly IMenuSceneChangerModel _menuSceneChangerModel;
    readonly FirebaseManager _firebaseManager;
    
    public MainMenuModel (
        IPlayerInfoModel playerInfoModel,
        IMenuSceneChangerModel menuSceneChangerModel,
        FirebaseManager firebaseManager
    )
    {
        _playerInfoModel = playerInfoModel;
        _menuSceneChangerModel = menuSceneChangerModel;
        _firebaseManager = firebaseManager;
    }

    public void EvaluateInitialState ()
    {
        if (!_firebaseManager.IsAuthenticated)
            ChangeMainMenuState(MainMenuState.Login);
        else
            ChangeMainMenuState(MainMenuState.Menu);
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

    public void Login (string email, string password)
    {
        _firebaseManager.Login(email, password, success =>
            {
                if (success)
                    ChangeMainMenuState(MainMenuState.Menu);
            }
        );
    }

    public void Register (string email, string password)
    {
        _firebaseManager.Register(email, password, success =>
            {
                if (success)
                    ChangeMainMenuState(MainMenuState.Menu);
            }
        );
    }
}