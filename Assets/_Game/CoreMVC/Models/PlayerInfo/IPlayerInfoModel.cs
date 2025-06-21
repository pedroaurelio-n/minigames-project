public interface IPlayerInfoModel
{
    int CurrentLives { get; }
    bool HasLivesRemaining { get; }
    int CurrentScore { get; }
    
    void Reset ();
    void ModifyLives (int amount);
    void ModifyScore (int amount);
    int GetLivesChangeType ();
    int GetScoreChangeType ();
}