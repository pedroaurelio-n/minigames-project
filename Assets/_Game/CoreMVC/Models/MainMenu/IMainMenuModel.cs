using System;

public interface IMainMenuModel
{
    event Action<MainMenuState> OnMainMenuStateChanged;
    
    int HighScore { get; }

    void ChangeMainMenuState (MainMenuState state);
    void PlayGame ();
    void SelectLevel (int index);
}