using System;
using System.Collections.Generic;
using System.Linq;

public class MiniGameSelectorModel : IMiniGameSelectorModel
{
    public List<MiniGameType> ActiveMiniGames { get; } = new();
    
    readonly IMiniGameSystemSettings _settings;
    readonly IMiniGameDifficultyModel _miniGameDifficultyModel;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly IRandomProvider _randomProvider;
    readonly List<MiniGameType> _availableTypes = new();

    int _currentMiniGameIndex;
    List<WeightedObject<MiniGameSkillTier>> _skillTierProbabilities;

    public MiniGameSelectorModel (
        IMiniGameSystemSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IGameSessionInfoProvider gameSessionInfoProvider,
        IRandomProvider randomProvider
    )
    {
        _settings = settings;
        _miniGameDifficultyModel = miniGameDifficultyModel;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _randomProvider = randomProvider;
    }

    public void Initialize ()
    {
        BuildActiveMiniGamesList();

        if (_gameSessionInfoProvider.CurrentContextType == ContextType.Menu)
            return;
        
        if (!_settings.MiniGameSkillPoolActive)
            ChooseUnpooledRandomMiniGame();
        else
            ChoosePooledRandomMiniGame();
    }
    
    void BuildActiveMiniGamesList ()
    {
        foreach (IMiniGameSkillTierSettings skillTierSettings in _settings.PoolSettings.SkillTierSettings)
            ActiveMiniGames.AddRange(skillTierSettings.ActiveMiniGames);
    }

    void ChooseUnpooledRandomMiniGame ()
    {
        for (int i = 0; i < ActiveMiniGames.Count; i++)
        {
            MiniGameType type = ActiveMiniGames[i];
            if (type == _gameSessionInfoProvider.CurrentMiniGameType)
            {
                _currentMiniGameIndex = i;
                
                if (!_settings.CanRepeatPrevious)
                    continue;
            }
            _availableTypes.Add(type);
        }

        if (_settings.RandomOrder)
        {
            _gameSessionInfoProvider.NextMiniGameType = _randomProvider.PickRandom(_availableTypes);
            return;
        }

        _currentMiniGameIndex++;
        if (_currentMiniGameIndex >= ActiveMiniGames.Count)
            _currentMiniGameIndex = 0;
        _gameSessionInfoProvider.NextMiniGameType = ActiveMiniGames[_currentMiniGameIndex];
    }

    void ChoosePooledRandomMiniGame ()
    {
        int targetDifficulty = _miniGameDifficultyModel.CurrentDifficultyLevel;
        
        IMiniGamePoolProbabilitySettings probabilitySettings = _settings.PoolSettings.ProbabilitySettings
            .Where(x => x.DifficultyLevel <= targetDifficulty)
            .OrderByDescending(x => x.DifficultyLevel)
            .FirstOrDefault();
        
        if (probabilitySettings == null)
        {
            probabilitySettings = _settings.PoolSettings.ProbabilitySettings
                .OrderBy(x => x.DifficultyLevel)
                .FirstOrDefault();
        }
        
        _skillTierProbabilities = new List<WeightedObject<MiniGameSkillTier>>();
        foreach (IMiniGamePoolChanceSettings chanceSettings in probabilitySettings.Chances)
        {
            _skillTierProbabilities.Add(
                new WeightedObject<MiniGameSkillTier>(chanceSettings.Tier, chanceSettings.Chance)
            );
        }

        MiniGameSkillTier selectedTier = _randomProvider.WeightedRandom(_skillTierProbabilities);

        IMiniGameSkillTierSettings skillTierSettings =
            _settings.PoolSettings.SkillTierSettings.FirstOrDefault(x => x.Tier == selectedTier);

        if (skillTierSettings == null)
        {
            DebugUtils.LogException(
                $"Trying to get invalid tier {selectedTier.ToString()} from pool skill tier settings."
            );
            return;
        }
        
        for (int i = 0; i < skillTierSettings.ActiveMiniGames.Count; i++)
        {
            MiniGameType type = skillTierSettings.ActiveMiniGames[i];
            if (type == _gameSessionInfoProvider.CurrentMiniGameType)
            {
                _currentMiniGameIndex = i;
                
                if (!_settings.CanRepeatPrevious)
                    continue;
            }
            _availableTypes.Add(type);
        }

        if (_settings.RandomOrder)
        {
            _gameSessionInfoProvider.NextMiniGameType = _randomProvider.PickRandom(_availableTypes);
            return;
        }
        
        _currentMiniGameIndex++;
        if (_currentMiniGameIndex >= skillTierSettings.ActiveMiniGames.Count)
            _currentMiniGameIndex = 0;
        _gameSessionInfoProvider.NextMiniGameType = skillTierSettings.ActiveMiniGames[_currentMiniGameIndex];
    }
}