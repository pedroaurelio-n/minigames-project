using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class GameSessionData
{
    //TODO pedro: create scene specific data classes
    [JsonProperty]
    public MiniGameData MiniGameData { get; set; }
    
    [JsonProperty]
    public MiniGameCurrentRunData MiniGameCurrentRunData { get; set; }
    
    [JsonProperty]
    public MetadataData MetadataData { get; set; }

    public GameSessionData ()
    {
        MiniGameData = new MiniGameData();
        MiniGameCurrentRunData = new MiniGameCurrentRunData();
        MetadataData = new MetadataData();
    }
    
    [JsonConstructor]
    public GameSessionData (
        MiniGameData miniGameData,
        MiniGameCurrentRunData miniGameCurrentRunData,
        MetadataData metadataData
    )
    {
        MiniGameData = miniGameData ?? new MiniGameData();
        MiniGameCurrentRunData = miniGameCurrentRunData ?? new MiniGameCurrentRunData();
        MetadataData = metadataData ?? new MetadataData();
    }
}