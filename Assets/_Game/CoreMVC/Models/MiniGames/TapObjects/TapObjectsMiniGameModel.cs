using System;
using UnityEngine;

public class TapObjectsMiniGameModel : BaseMiniGameModel, ITapObjectsMiniGameModel
{
    public event Action<IPressable, Vector2> OnTapPerformed;

    public int BaseObjectsToSpawn => _settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.TapObjects;
    public override TouchInputType InputTypes => TouchInputType.Tap;

    readonly IPressModel _pressModel;

    public TapObjectsMiniGameModel (
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

    void HandleTapPerformed (IPressable pressable, Vector2 tapPosition)
    {
        OnTapPerformed?.Invoke(pressable, tapPosition);
    }
}