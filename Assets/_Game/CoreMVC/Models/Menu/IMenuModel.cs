using System;

public interface IMenuModel : IDisposable
{
    IMainMenuModel MainMenuModel { get; }
    IGameOverModel GameOverModel { get; }
}