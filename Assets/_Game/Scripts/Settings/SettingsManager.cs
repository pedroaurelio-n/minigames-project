public class SettingsManager
{
    public SettingsProvider<IMiniGameSystemSettings, MiniGameSystemSettings> MiniGameSettings
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
        MiniGameSettings = new SettingsProvider<IMiniGameSystemSettings, MiniGameSystemSettings>("mini-game-system-settings");
    }
}