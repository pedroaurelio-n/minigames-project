using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameSettings : IMiniGameSettings
{
    [JsonProperty]
    public bool RandomOrder { get; }
    
    [JsonProperty]
    public IReadOnlyList<MiniGameType> ActiveMiniGames { get; }

    [JsonConstructor]
    public MiniGameSettings (
        bool randomOrder,
        MiniGameType[] activeMiniGames
    )
    {
        RandomOrder = randomOrder;
        ActiveMiniGames = activeMiniGames;
    }
}