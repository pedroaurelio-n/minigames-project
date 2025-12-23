using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameDifficultySettings : IMiniGameDifficultySettings
{
    [JsonProperty]
    public int MaxDifficultyLevelIndex { get; }
    
    [JsonProperty] 
    public int MiniGameEvaluationRange { get; }

    [JsonProperty] 
    public int WinAmountToIncreaseLevel { get; }

    [JsonProperty] 
    public float TimerDecreasePerStep { get; }

    [JsonProperty] 
    public int TimerDecreaseStep { get; }

    [JsonProperty] 
    public bool EnableDifficultyVariance { get; }

    [JsonProperty] 
    public float DifficultyVarianceChance { get; }

    [JsonProperty] 
    public bool DropLevelOnLoss { get; }
    
    [JsonProperty] 
    public int LossAmountToDropLevel { get; }

    public MiniGameDifficultySettings (
        int maxDifficultyLevelIndex,
        int miniGameEvaluationRange,
        int winAmountToIncreaseLevel,
        float timerDecreasePerStep,
        int timerDecreaseStep,
        bool enableDifficultyVariance,
        float difficultyVarianceChance,
        bool dropLevelOnLoss,
        int lossAmountToDropLevel
    )
    {
        MaxDifficultyLevelIndex = maxDifficultyLevelIndex;
        MiniGameEvaluationRange = miniGameEvaluationRange;
        WinAmountToIncreaseLevel = winAmountToIncreaseLevel;
        TimerDecreasePerStep = timerDecreasePerStep;
        TimerDecreaseStep = timerDecreaseStep;
        EnableDifficultyVariance = enableDifficultyVariance;
        DifficultyVarianceChance = difficultyVarianceChance;
        DropLevelOnLoss = dropLevelOnLoss;
        LossAmountToDropLevel = lossAmountToDropLevel;
    }
}