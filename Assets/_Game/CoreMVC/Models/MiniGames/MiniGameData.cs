using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameData
{
    [JsonProperty]
    public int HighScore { get; set; }
    
    [JsonProperty]
    public Dictionary<string, int> VictoriesInMiniGame { get; set; }
    
    [JsonProperty]
    public Dictionary<string, int> DefeatsInMiniGame { get; set; }

    public MiniGameData ()
    {
        VictoriesInMiniGame = new Dictionary<string, int>();
        DefeatsInMiniGame = new Dictionary<string, int>();
    }

    [JsonConstructor]
    public MiniGameData (
        int highScore,
        Dictionary<string, int> victoriesInMiniGame,
        Dictionary<string, int> defeatsInMiniGame
    )
    {
        HighScore = highScore;
        VictoriesInMiniGame = victoriesInMiniGame;
        DefeatsInMiniGame = defeatsInMiniGame;
    }
}