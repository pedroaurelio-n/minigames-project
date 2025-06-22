using System;
using UnityEngine;

public class TapFloatingMiniGameModel : BaseMiniGameModel, ITapFloatingMiniGameModel
{
    public event Action<IPressable, Vector2> OnTapPerformed;

    public int BaseTargetsToSpawn => _settings.BaseObjectiveMilestone.Value;
    public int BaseObjectsToSpawn => _settings.BaseObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.TapFloating;
    public override TouchInputType InputTypes => TouchInputType.Tap;

    readonly IPressModel _pressModel;

    public TapFloatingMiniGameModel (
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