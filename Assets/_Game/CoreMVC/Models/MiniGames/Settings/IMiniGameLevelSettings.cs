public interface IMiniGameLevelSettings
{
    int Level { get; }
    int? ObjectCount { get; }
    int? MilestoneCount { get; }
    float? TimerModifier { get; }
    float? SpeedModifier { get; }
    float? RateModifier { get; }
}