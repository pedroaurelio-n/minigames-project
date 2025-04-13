public class PlayerInfoModel : IPlayerInfoModel
{
    //TODO pedro: move to options/settings
    const int START_LIVES = 5;

    public int CurrentLives { get; private set; }
    public bool HasLivesRemaining => CurrentLives > 0;
    public int CurrentScore { get; private set; }

    int _previousLives;
    int _previousScore;
    
    public void Initialize ()
    {
        CurrentLives = START_LIVES;
        CurrentScore = 0;
    }

    public void Reset () => Initialize();
    
    public void ModifyLives (int amount)
    {
        _previousLives = CurrentLives;
        CurrentLives += amount;
    }

    public void ModifyScore (int amount)
    {
        _previousScore = CurrentScore;
        CurrentScore += amount;
    }

    public int GetLivesChangeType ()
    {
        int changeType = _previousLives == CurrentLives || CurrentLives == START_LIVES
            ? 0
            : _previousLives < CurrentLives
                ? 1
                : -1;
        _previousLives = CurrentLives;
        return changeType;
    }

    public int GetScoreChangeType ()
    {
        int changeType = _previousScore == CurrentScore ? 0 : _previousScore < CurrentScore ? 1 : -1;
        _previousScore = CurrentScore;
        return changeType;
    }
}