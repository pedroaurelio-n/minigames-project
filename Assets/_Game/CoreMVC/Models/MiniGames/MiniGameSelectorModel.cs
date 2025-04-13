using System;
using System.Collections.Generic;

public class MiniGameSelectorModel : IMiniGameSelectorModel
{
    public MiniGameType NextType { get; private set; }

    readonly IGameSessionInfoProvider _gameSessionInfoProvider;
    readonly IRandomProvider _randomProvider;
    readonly List<MiniGameType> _availableTypes = new();

    public MiniGameSelectorModel (
        IGameSessionInfoProvider gameSessionInfoProvider,
        IRandomProvider randomProvider
    )
    {
        _gameSessionInfoProvider = gameSessionInfoProvider;
        _randomProvider = randomProvider;
    }

    public void Initialize ()
    {
        foreach (MiniGameType type in Enum.GetValues(typeof(MiniGameType)))
        {
            if (type == _gameSessionInfoProvider.CurrentMiniGameType)
                continue;
            _availableTypes.Add(type);
        }
        
        NextType = _availableTypes[_randomProvider.Range(0, _availableTypes.Count)];
    }
}