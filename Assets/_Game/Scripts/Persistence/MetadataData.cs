using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MetadataData
{
    [JsonProperty]
    public string GameVersion { get; set; }
    
    [JsonProperty]
    public DateTime LastPlayedTime { get; set; }

    public MetadataData ()
    {
    }

    [JsonConstructor]
    public MetadataData (
        string gameVersion,
        DateTime lastPlayedTime
    )
    {
        GameVersion = gameVersion;
        LastPlayedTime = lastPlayedTime;
    }
}