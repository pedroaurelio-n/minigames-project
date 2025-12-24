using System;
using UnityEngine;

public class TapFloatingMiniGameModel : BaseMiniGameModel, ITapFloatingMiniGameModel
{
    public event Action<ITappable, Vector2> OnTapPerformed;

    public int BaseTargetsToSpawn => CurrentLevelSettings.MilestoneCount.Value;
    public int BaseObjectsToSpawn => CurrentLevelSettings.ObjectCount.Value;
    
    public override MiniGameType Type => MiniGameType.TapFloating;
    public override TouchInputType InputTypes => TouchInputType.Tap;

    readonly IPressModel _pressModel;

    public TapFloatingMiniGameModel (
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