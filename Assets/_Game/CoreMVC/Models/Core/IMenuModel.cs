using System;

public interface IMenuModel : IDisposable
{
    IMainMenuModel MainMenuModel { get; }
    IGameOverModel GameOverModel { get; }
    IMiniGameDifficultyModel MiniGameDifficultyModel { get; }
    IMiniGameSelectorModel MiniGameSelectorModel { get; }

    void Initialize ();
    void LateInitialize ();
}