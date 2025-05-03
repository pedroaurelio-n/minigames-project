public class SettingsManager
{
    public SettingsObject<IPlayerSettings, PlayerSettings> PlayerSettings
    {
        get;
        private set;
    }
    
    public SettingsObject<IMiniGameSystemSettings, MiniGameSystemSettings> MiniGameSystemSettings
    {
        get;
        private set;
    }

    public IndexedSettingsProvider<IMiniGameSettings, MiniGameSettings> MiniGameSettingsProvider
    {
        get;
        private set;
    }

    public SettingsManager ()
    {
        InitializeSettings();
    }

    void InitializeSettings ()
    {
        PlayerSettings = new SettingsObject<IPlayerSettings, PlayerSettings>("player-settings");
        
        MiniGameSystemSettings =
            new SettingsObject<IMiniGameSystemSettings, MiniGameSystemSettings>("mini-game-system-settings");

        MiniGameSettingsProvider = new IndexedSettingsProvider<IMiniGameSettings, MiniGameSettings>(
            "mini-game-settings-",
            "miniGameSettings",
            true
        );
    }
}