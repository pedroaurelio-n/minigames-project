using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSystemSettings : IMiniGameSystemSettings
{
    [JsonProperty]
    public float BaseDuration { get; }

    [JsonProperty]
    public float EndGraceDuration { get; }
    
    [JsonProperty]
    public float NextMiniGameDelay { get; }

    [JsonProperty]
    public bool RandomOrder { get; }
    
    [JsonProperty]
    public bool CanRepeatPrevious { get; }
    
    [JsonProperty]
    public IReadOnlyList<MiniGameType> ActiveMiniGames { get; }

    [JsonConstructor]
    public MiniGameSystemSettings (
        float baseDuration,
        float endGraceDuration,
        float nextMiniGameDelay,
        bool randomOrder,
        bool canRepeatPrevious,
        MiniGameType[] activeMiniGames
    )
    {
        BaseDuration = baseDuration;
        EndGraceDuration = endGraceDuration;
        NextMiniGameDelay = nextMiniGameDelay;
        RandomOrder = randomOrder;
        CanRepeatPrevious = canRepeatPrevious;
        ActiveMiniGames = activeMiniGames;
    }
}