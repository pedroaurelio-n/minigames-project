using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class GameSessionData
{
    //TODO pedro: create scene specific data classes
    [JsonProperty]
    public MetadataData MetadataData { get; set; }
    
    [JsonProperty]
    public MiniGameData MiniGameData { get; set; }

    public GameSessionData ()
    {
        MetadataData = new MetadataData();
        MiniGameData = new MiniGameData();
    }
    
    [JsonConstructor]
    public GameSessionData (
        MetadataData metadataData,
        MiniGameData miniGameData
    )
    {
        MetadataData = metadataData ?? new MetadataData();
        MiniGameData = miniGameData ?? new MiniGameData();
    }
}