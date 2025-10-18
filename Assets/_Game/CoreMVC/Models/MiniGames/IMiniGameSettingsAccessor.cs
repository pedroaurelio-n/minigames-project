public interface IMiniGameSettingsAccessor
{
    IMiniGameSettings GetSettingsFromCurrentMinigame ();
    IMiniGameSettings GetSettingsByType (MiniGameType type);
    IMiniGameSettings GetSettingsByIndex (int index);
}