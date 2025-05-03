using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSystemSettings : IMiniGameSystemSettings
{
    [JsonProperty]
    public bool RandomOrder { get; }
    
    [JsonProperty]
    public bool CanRepeatPrevious { get; }
    
    [JsonProperty]
    public IReadOnlyList<MiniGameType> ActiveMiniGames { get; }

    [JsonConstructor]
    public MiniGameSystemSettings (
        bool randomOrder,
        bool canRepeatPrevious,
        MiniGameType[] activeMiniGames
    )
    {
        RandomOrder = randomOrder;
        CanRepeatPrevious = canRepeatPrevious;
        ActiveMiniGames = activeMiniGames;
    }
}