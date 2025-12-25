public interface IMiniGameSystemSettings
{
    IMiniGameTimingSettings TimingSettings { get; }
    IMiniGameDifficultySettings DifficultySettings { get; }
    bool RandomOrder { get; }
    bool CanRepeatPrevious { get; }
    bool MiniGameSkillPoolActive { get; }
    IMiniGamePoolSettings PoolSettings { get; }
}