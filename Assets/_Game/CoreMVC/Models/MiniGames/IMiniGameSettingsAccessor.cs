public interface IMiniGameSettingsAccessor
{
    IMiniGameSettings GetSettingsByType (MiniGameType type);
    IMiniGameSettings GetSettingsByIndex (int index);
}