//TODO pedro: this class seems useless/refactorable into a cleaner model
public class PlayerInfoModel : IPlayerInfoModel
{
    public int CurrentLives { get; private set; }
    public bool HasLivesRemaining => CurrentLives > 0;
    public int CurrentScore { get; private set; }

    readonly GameSessionData _gameSessionData;
    readonly IPlayerSettings _settings;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    int _previousLives;
    int _previousScore;

    public PlayerInfoModel (
        GameSessionData gameSessionData,
        IPlayerSettings settings,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _gameSessionData = gameSessionData;
        _settings = settings;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }

    public void Reset ()
    {
        _previousLives = 0;
        _previousScore = 0;
        
        CurrentLives = _settings.StartingLives;
        CurrentScore = 0;
    }

    public void ModifyLives (int amount)
    {
        if (!_gameSessionInfoProvider.HasStartedGameRun)
            return;
        
        _previousLives = CurrentLives;
        CurrentLives += amount;
    }

    public void ModifyScore (int amount)
    {
        if (!_gameSessionInfoProvider.HasStartedGameRun)
            return;
        
        _previousScore = CurrentScore;
        CurrentScore += amount;
        
        if (CurrentScore > _gameSessionData.HighScore)
            _gameSessionData.HighScore = CurrentScore;
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