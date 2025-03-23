using System;

public class MiniGameModelFactory : IMiniGameModelFactory
{
    readonly IMiniGameTimerModel _miniGameTimerModel;

    public MiniGameModelFactory (
        IMiniGameTimerModel miniGameTimerModel
    )
    {
        _miniGameTimerModel = miniGameTimerModel;
    }
    
    public IMiniGameModel CreateMiniGameBasedOnType (MiniGameType type)
    {
        switch (type)
        {
            case MiniGameType.TapObjects:
                ITapObjectsMiniGameModel tapMiniGameModel = new TapObjectsMiniGameModel(_miniGameTimerModel);
                return tapMiniGameModel;
            case MiniGameType.DragObjects:
                return null;
            case MiniGameType.ThrowObjects:
                return null;
            case MiniGameType.LongPressObjects:
                return null;
            case MiniGameType.FindObject:
                return null;
            case MiniGameType.TurnShooter:
                return null;
            case MiniGameType.Runner:
                return null;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}