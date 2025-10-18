public class MiniGameSettingsAccessor : IMiniGameSettingsAccessor
{
    readonly IndexedSettingsProvider<IMiniGameSettings, MiniGameSettings> _miniGameSettingsProvider;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    public MiniGameSettingsAccessor (
        SettingsManager settingsManager,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _miniGameSettingsProvider = settingsManager.MiniGameSettingsProvider;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }

    public IMiniGameSettings GetSettingsFromCurrentMinigame ()
    {
        return GetSettingsByType(_gameSessionInfoProvider.CurrentMiniGameType);
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