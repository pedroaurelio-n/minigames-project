using System;
using UnityEngine;

public class TapMovingMiniGameModel : BaseMiniGameModel, ITapMovingMinigameModel
{
    public event Action<ITappable, Vector2> OnTapPerformed;

    public int BaseObjectsToSpawn => _settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.TapMoving;
    public override TouchInputType InputTypes => TouchInputType.Tap;

    readonly IPressModel _pressModel;

    public TapMovingMiniGameModel (
        IMiniGameSettings settings,
        IMiniGameTimerModel miniGameTimerModel,
        IPressModel pressModel
    ) : base(settings, miniGameTimerModel)
    {
        _pressModel = pressModel;
    }

    protected override void AddListeners ()
    {
        base.AddListeners();
        _pressModel.OnTapPerformed += HandleTapPerformed;
    }

    protected override void RemoveListeners ()
    {
        base.RemoveListeners();
        _pressModel.OnTapPerformed -= HandleTapPerformed;
    }

    void HandleTapPerformed (ITappable tappable, Vector2 tapPosition)
    {
        OnTapPerformed?.Invoke(tappable, tapPosition);
    }
}