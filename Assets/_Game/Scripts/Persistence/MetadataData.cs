using System;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MetadataData
{
    [JsonProperty]
    public DateTime LastPlayedTime { get; set; }
    
    [JsonProperty]
    public GameVersion GameVersion { get; set; }

    public MetadataData ()
    {
        GameVersion = new GameVersion();
    }

    [JsonConstructor]
    public MetadataData (
        DateTime lastPlayedTime,
        GameVersion gameVersion
    )
    {
        LastPlayedTime = lastPlayedTime;
        GameVersion = gameVersion;
    }
}