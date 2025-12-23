using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiniGameDifficultyModel : IMiniGameDifficultyModel
{
    public int CurrentDifficultyLevel => _variantDifficulty ?? _data.CurrentDifficultyLevel;
    public float CurrentTimerDecrease { get; private set; }
    public bool IsVariantLevel => _variantDifficulty.HasValue;
    
    readonly MiniGameCurrentRunData _data;
    readonly IMiniGameSystemSettings _settings;
    readonly IRandomProvider _randomProvider;
    
    IMiniGameManagerModel _miniGameManagerModel;

    int? _variantDifficulty;

    public MiniGameDifficultyModel (
        MiniGameCurrentRunData data,
        IMiniGameSystemSettings settings,
        IRandomProvider randomProvider
    )
    {
        _data = data;
        _settings = settings;
        _randomProvider = randomProvider;
    }

    public void UpdateDependencies (IMiniGameManagerModel miniGameManagerModel)
    {
        _miniGameManagerModel = miniGameManagerModel;
    }

    public void Initialize ()
    {
        AddListeners();
        EvaluateTimerDecrease();
        EvaluateVariantDifficulty();
    }
    
    void RegisterMiniGameWin ()
    {
        _data.ConsecutiveLosses = 0;
        _data.ConsecutiveWins++;
        _data.TotalWins++;
        
        UpdateLastResults(true);
        EvaluateNewDifficulty(false);
    }

    void RegisterMiniGameLoss ()
    {
        _data.ConsecutiveWins = 0;
        _data.ConsecutiveLosses++;
        _data.TotalLosses++;
        
        UpdateLastResults(false);
        EvaluateNewDifficulty(true);
    }
    
    void UpdateLastResults (bool gameWin)
    {
        _data.LastResults.Add(gameWin);
        
        while (_data.LastResults.Count > _settings.DifficultySettings.MiniGameEvaluationRange)
            _data.LastResults.RemoveAt(0);
    }

    void EvaluateNewDifficulty (bool hasLost)
    {
        if (hasLost && _settings.DifficultySettings.DropLevelOnLoss)
        {
            if (_data.LastResults.Count(x => !x) < _settings.DifficultySettings.LossAmountToDropLevel)
                return;

            _data.LastResults = new List<bool>();
            _data.CurrentDifficultyLevel = Mathf.Max(1, _data.CurrentDifficultyLevel - 1);
            return;
        }
        
        if (!hasLost)
        {
            if (_data.LastResults.Count(x => x) < _settings.DifficultySettings.WinAmountToIncreaseLevel)
                return;
            
            _data.LastResults = new List<bool>();
            _data.CurrentDifficultyLevel = Mathf.Min(
                _settings.DifficultySettings.MaxDifficultyLevelIndex,
                _data.CurrentDifficultyLevel + 1
            );
        }
    }

    void EvaluateVariantDifficulty ()
    {
        if (!_settings.DifficultySettings.EnableDifficultyVariance)
        {
            _variantDifficulty = null;
            return;
        }

        if (_randomProvider.Value > _settings.DifficultySettings.DifficultyVarianceChance)
            return;

        _variantDifficulty = Mathf.Min(
            _settings.DifficultySettings.MaxDifficultyLevelIndex,
            _data.CurrentDifficultyLevel + 1
        );
    }

    void EvaluateTimerDecrease ()
    {
        int totalPlayedMiniGames = _data.TotalWins + _data.TotalLosses;
        int currentTimerStep = (int)(totalPlayedMiniGames / _settings.DifficultySettings.TimerDecreaseStep);
        CurrentTimerDecrease = currentTimerStep * _settings.DifficultySettings.TimerDecreasePerStep;
    }

    void AddListeners ()
    {
        _miniGameManagerModel.OnMiniGameEnded += HandleMiniGameEnded;
    }

    void RemoveListeners ()
    {
        _miniGameManagerModel.OnMiniGameEnded -= HandleMiniGameEnded;
    }

    void HandleMiniGameEnded (bool hasCompleted, bool isDuringGameRun)
    {
        if (!isDuringGameRun)
            return;

        if (hasCompleted)
            RegisterMiniGameWin();
        else
            RegisterMiniGameLoss();
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}