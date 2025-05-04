public interface IMiniGameSettings
{
    string Name { get; }
    string Instructions { get; }
    int? BaseObjectCount { get; }
    int? BaseObjectiveMilestone { get; }
}