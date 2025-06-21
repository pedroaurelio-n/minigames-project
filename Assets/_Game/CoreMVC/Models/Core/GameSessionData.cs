using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class GameSessionData
{
    //TODO pedro: create scene specific data classes
    [JsonProperty]
    public int HighScore { get; set; }
    
    [JsonProperty]
    public MetadataData MetadataData { get; set; }

    public GameSessionData ()
    {
        MetadataData = new MetadataData();
    }
    
    [JsonConstructor]
    public GameSessionData (
        int highScore,
        MetadataData metadata
    )
    {
        HighScore = highScore;
        MetadataData = metadata ?? new MetadataData();
    }
}