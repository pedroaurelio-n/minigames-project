public interface IMiniGameDifficultySettings
{
    int MaxDifficultyLevelIndex { get; }
    int MiniGameEvaluationRange { get; }
    int WinAmountToIncreaseLevel { get; }
    float TimerDecreasePerStep { get; }
    int TimerDecreaseStep { get; }
    bool EnableDifficultyVariance { get; }
    float DifficultyVarianceChance { get; }
    bool DropLevelOnLoss { get; }
    int LossAmountToDropLevel { get; }
}