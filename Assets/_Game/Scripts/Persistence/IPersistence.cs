using System;

public interface IPersistence
{
    GameSessionData Data { get; }
    
    void InitializeData (GameSessionData data, GameVersion gameVersion, Action afterNullLoad);
}