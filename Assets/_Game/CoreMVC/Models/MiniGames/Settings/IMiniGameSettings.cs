using System.Collections.Generic;

public interface IMiniGameSettings
{
    string Name { get; }
    string StringId { get; }
    bool HasCustomScene { get; }
    string Instructions { get; }
    IReadOnlyList<IMiniGameLevelSettings> LevelSettings { get; }
}