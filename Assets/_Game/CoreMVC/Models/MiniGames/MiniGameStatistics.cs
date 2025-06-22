public struct MiniGameStatistics
{
    public string StringId { get; }
    public string Name { get; }
    public MiniGameType Type { get; }
    public int VictoryCount { get; }
    public int DefeatCount { get; }

    public MiniGameStatistics (
        string stringId,
        string name,
        MiniGameType type,
        int victoryCount,
        int defeatCount
    )
    {
        StringId = stringId;
        Name = name;
        Type = type;
        VictoryCount = victoryCount;
        DefeatCount = defeatCount;
    }
}