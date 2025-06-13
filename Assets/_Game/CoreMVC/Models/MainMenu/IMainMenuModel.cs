using System;

public interface IMainMenuModel
{
    event Action<MainMenuState> OnMainMenuStateChanged;
    
    int HighScore { get; }
    string User { get; }

    void EvaluateInitialState ();
    void ChangeMainMenuState (MainMenuState state);
    void PlayGame ();
    void SelectLevel (int index);
    void Login (string email, string password);
    void Register (string email, string password);
}