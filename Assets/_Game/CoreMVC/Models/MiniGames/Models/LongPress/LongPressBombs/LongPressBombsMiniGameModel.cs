using System;
using UnityEngine;

public class LongPressBombsMiniGameModel : BaseMiniGameModel, ILongPressBombsMiniGameModel
{
    public event Action<ILongPressable, Vector2> OnLongPressBegan;
    public event Action<ILongPressable, Vector2> OnLongPressCancelled;
    public event Action<ILongPressable, Vector2, float> OnLongPressEnded;

    public int BaseObjectsToSpawn => _Settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.LongPressBombs;
    public override TouchInputType InputTypes => TouchInputType.LongPress;

    readonly IPressModel _pressModel;

    public LongPressBombsMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
        IMiniGameTimerModel miniGameTimerModel,
        IPressModel pressModel
    ) : base(settings, miniGameDifficultyModel, miniGameTimerModel)
    {
        _pressModel = pressModel;
    }

    protected override void AddListeners ()
    {
        base.AddListeners();
        _pressModel.OnLongPressBegan += HandleLongPressBegan;
        _pressModel.OnLongPressCancelled += HandleLongPressCancelled;
        _pressModel.OnLongPressEnded += HandleLongPressEnded;
    }

    protected override void RemoveListeners ()
    {
        base.RemoveListeners();
        _pressModel.OnLongPressBegan -= HandleLongPressBegan;
        _pressModel.OnLongPressCancelled -= HandleLongPressCancelled;
        _pressModel.OnLongPressEnded -= HandleLongPressEnded;
    }

    void HandleLongPressBegan (ILongPressable longPressable, Vector2 pressPosition)
    {
        OnLongPressBegan?.Invoke(longPressable, pressPosition);
    }
    
    void HandleLongPressCancelled (ILongPressable longPressable, Vector2 pressPosition)
    {
        OnLongPressCancelled?.Invoke(longPressable, pressPosition);
    }
    
    void HandleLongPressEnded (ILongPressable longPressable, Vector2 pressPosition, float duration)
    {
        OnLongPressEnded?.Invoke(longPressable, pressPosition, duration);
    }
}