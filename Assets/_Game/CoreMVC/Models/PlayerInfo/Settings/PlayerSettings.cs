using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class PlayerSettings : IPlayerSettings
{
    [JsonProperty]
    public int StartingLives { get; }

    [JsonConstructor]
    public PlayerSettings (int startingLives)
    {
        StartingLives = startingLives;
    }
}