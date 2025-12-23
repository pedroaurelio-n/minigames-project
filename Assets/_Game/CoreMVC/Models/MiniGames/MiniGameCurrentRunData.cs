using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class MiniGameCurrentRunData
{
    [JsonProperty]
    public int CurrentDifficultyLevel { get; set; }
    
    [JsonProperty]
    public int ConsecutiveWins { get; set; }
    
    [JsonProperty]
    public int ConsecutiveLosses { get; set; }
    
    [JsonProperty]
    public int TotalWins { get; set; }
    
    [JsonProperty]
    public int TotalLosses { get; set; }
    
    [JsonProperty]
    public List<bool> LastResults { get; set; }

    public MiniGameCurrentRunData ()
    {
        LastResults = new List<bool>();
    }
    
    [JsonConstructor]
    public MiniGameCurrentRunData (
        int currentDifficultyLevel,
        int consecutiveWins,
        int consecutiveLosses,
        int totalWins,
        int totalLosses,
        List<bool> lastResults
    )
    {
        CurrentDifficultyLevel = currentDifficultyLevel;
        ConsecutiveWins = consecutiveWins;
        ConsecutiveLosses = consecutiveLosses;
        TotalWins = totalWins;
        TotalLosses = totalLosses;
        LastResults = lastResults;
    }

    public void Reset ()
    {
        CurrentDifficultyLevel = 1;
        ConsecutiveWins = 0;
        ConsecutiveLosses = 0;
        TotalWins = 0;
        TotalLosses = 0;
        LastResults = new List<bool>();
    }
}