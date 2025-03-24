using System;
using UnityEngine;

public class TapObjectsMiniGameModel : BaseMiniGameModel, ITapObjectsMiniGameModel
{
    //TODO pedro: move to settings or options
    const int OBJECTS_TO_SPAWN = 5;
    const int MAX_DISTANCE = 20;

    public event Action<IPressable, Vector2> OnTapPerformed;

    public int BaseObjectsToSpawn => OBJECTS_TO_SPAWN;
    public int MaxSpawnDistance => MAX_DISTANCE;
    
    public override MiniGameType Type => MiniGameType.TapObjects;
    public override TouchInputType InputTypes => TouchInputType.Tap;
    public override string Instructions => "Tap all objects to win!";

    readonly IPressModel _pressModel;

    public TapObjectsMiniGameModel (
        IMiniGameTimerModel miniGameTimerModel,
        IPressModel pressModel
    ) : base(miniGameTimerModel)
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