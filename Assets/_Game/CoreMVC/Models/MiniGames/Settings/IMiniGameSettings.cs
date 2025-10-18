public interface IMiniGameSettings
{
    string Name { get; }
    string StringId { get; }
    bool HasCustomScene { get; }
    string Instructions { get; }
    int? BaseObjectCount { get; }
    int? BaseObjectiveMilestone { get; }
}