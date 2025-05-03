using System;
using System.Collections.Generic;

public class MiniGameSelectorModel : IMiniGameSelectorModel
{
    readonly IMiniGameSystemSettings _settings;
    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly IRandomProvider _randomProvider;
    readonly List<MiniGameType> _availableTypes = new();

    int _currentMiniGameIndex;

    public MiniGameSelectorModel (
        IMiniGameSystemSettings settings,
        IGameSessionInfoProvider gameSessionInfoProvider,
        IRandomProvider randomProvider
    )
    {
        _settings = settings;
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _randomProvider = randomProvider;
    }

    public void Initialize ()
    {
        for (int i = 0; i < _settings.ActiveMiniGames.Count; i++)
        {
            MiniGameType type = _settings.ActiveMiniGames[i];
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
        if (_currentMiniGameIndex >= _settings.ActiveMiniGames.Count)
            _currentMiniGameIndex = 0;
        _gameSessionInfoProvider.NextMiniGameType = _settings.ActiveMiniGames[_currentMiniGameIndex];
    }
}