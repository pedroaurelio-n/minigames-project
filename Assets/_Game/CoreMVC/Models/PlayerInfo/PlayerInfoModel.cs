public class PlayerInfoModel : IPlayerInfoModel
{
    public int CurrentLives { get; private set; }
    public bool HasLivesRemaining => CurrentLives > 0;
    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    readonly IPlayerSettings _settings;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    int _previousLives;
    int _previousScore;

    public PlayerInfoModel (
        IPlayerSettings settings,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _settings = settings;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }
    
    public void Initialize ()
    {
        CurrentLives = _settings.StartingLives;
        CurrentScore = 0;
    }

    public void Reset ()
    {
        _previousLives = 0;
        _previousScore = 0;
        Initialize();
    }

    public void ModifyLives (int amount)
    {
        _previousLives = CurrentLives;
        CurrentLives += amount;
        
        if (!HasLivesRemaining)
            _gameSessionInfoProvider.HasStartedGameRun = false;
    }

    public void ModifyScore (int amount)
    {
        _previousScore = CurrentScore;
        CurrentScore += amount;
        
        if (CurrentScore > HighScore)
            HighScore = CurrentScore;
    }

    public int GetLivesChangeType ()
    {
        int changeType = _previousLives == CurrentLives || CurrentLives == _settings.StartingLives
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