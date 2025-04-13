public class SettingsManager
{
    public SettingsProvider<IMiniGameSettings, MiniGameSettings> MiniGameSettings
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
        MiniGameSettings = new SettingsProvider<IMiniGameSettings, MiniGameSettings>("mini-game-settings");
    }
}