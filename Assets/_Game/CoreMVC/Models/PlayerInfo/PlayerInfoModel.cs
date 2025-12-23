//TODO pedro: this class seems useless/refactorable into a cleaner model
public class PlayerInfoModel : IPlayerInfoModel
{
    public int CurrentLives { get; private set; }
    public bool HasLivesRemaining => CurrentLives > 0;
    public int CurrentScore { get; private set; }

    readonly MiniGameData _miniGameData;
    readonly MiniGameCurrentRunData _miniGameCurrentRunData;
    readonly IPlayerSettings _playerSettings;
    readonly IMiniGameSystemSettings _miniGameSystemSettings;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;

    int _previousLives;
    int _previousScore;

    public PlayerInfoModel (
        MiniGameData miniGameData,
        MiniGameCurrentRunData miniGameCurrentRunData,
        IPlayerSettings playerSettings,
        IMiniGameSystemSettings miniGameSystemSettings,
        IGameSessionInfoProvider gameSessionInfoProvider
    )
    {
        _miniGameData = miniGameData;
        _miniGameCurrentRunData = miniGameCurrentRunData;
        _playerSettings = playerSettings;
        _miniGameSystemSettings = miniGameSystemSettings;
        _gameSessionInfoProvider = gameSessionInfoProvider;
    }

    public void Reset ()
    {
        _previousLives = 0;
        _previousScore = 0;
        
        CurrentLives = _playerSettings.StartingLives;
        CurrentScore = 0;
        
        //TODO pedro: maybe isolate in a different (context) model
        _miniGameCurrentRunData.Reset();
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
        
        if (CurrentScore > _miniGameData.HighScore)
            _miniGameData.HighScore = CurrentScore;
    }

    public int GetLivesChangeType ()
    {
        int changeType = _previousLives == CurrentLives || CurrentLives == _playerSettings.StartingLives
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