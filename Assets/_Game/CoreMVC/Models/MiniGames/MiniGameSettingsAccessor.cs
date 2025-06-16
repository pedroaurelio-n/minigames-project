public class MiniGameSettingsAccessor : IMiniGameSettingsAccessor
{
    readonly IndexedSettingsProvider<IMiniGameSettings, MiniGameSettings> _miniGameSettingsProvider;

    public MiniGameSettingsAccessor (SettingsManager settingsManager)
    {
        _miniGameSettingsProvider = settingsManager.MiniGameSettingsProvider;
    }
    
    public IMiniGameSettings GetSettingsByType (MiniGameType type)
    {
        return _miniGameSettingsProvider.GetSettingsByIndex((int)type).Instance;
    }

    public IMiniGameSettings GetSettingsByIndex (int index)
    {
        return _miniGameSettingsProvider.GetSettingsByIndex(index).Instance;
    }
}