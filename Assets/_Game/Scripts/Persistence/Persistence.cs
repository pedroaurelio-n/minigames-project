using System;

public class Persistence : IPersistence
{
    public GameSessionData Data { get; private set; }

    public void InitializeData (GameSessionData data, GameVersion gameVersion, Action afterNullLoad)
    {
        bool nullLoad = data == null;
        data ??= new GameSessionData();
        
        Data = data;
        Data.MetadataData.GameVersion = gameVersion.ToString();

        if (nullLoad)
            afterNullLoad();
    }
}