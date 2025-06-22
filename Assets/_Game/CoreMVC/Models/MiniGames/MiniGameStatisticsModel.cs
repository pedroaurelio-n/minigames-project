public class MiniGameStatisticsModel : IMiniGameStatisticsModel
{
    readonly MiniGameData _data;
    readonly IMiniGameSettingsAccessor _miniGameSettingsAccessor;

    public MiniGameStatisticsModel (
        MiniGameData data,
        IMiniGameSettingsAccessor miniGameSettingsAccessor
    )
    {
        _data = data;
        _miniGameSettingsAccessor = miniGameSettingsAccessor;
    }

    public MiniGameStatistics GetMiniGameStatisticsByType (MiniGameType miniGameType)
    {
        IMiniGameSettings settings = _miniGameSettingsAccessor.GetSettingsByType(miniGameType);
        string stringId = settings.StringId.ToFirstCharLower();

        _data.VictoriesInMiniGame.TryGetValue(stringId, out int victoryCount);
        _data.DefeatsInMiniGame.TryGetValue(stringId, out int defeatCount);
        
        MiniGameStatistics statistics = new(
            settings.StringId,
            settings.Name,
            miniGameType,
            victoryCount,
            defeatCount
        );

        return statistics;
    }
}