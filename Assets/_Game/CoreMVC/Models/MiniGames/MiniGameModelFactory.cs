using System;

public class MiniGameModelFactory : IMiniGameModelFactory
{
    readonly IMiniGameTimerModel _miniGameTimerModel;
    readonly ITouchInputModel _touchInputModel;
    readonly IPressModel _pressModel;

    public MiniGameModelFactory (
        IMiniGameTimerModel miniGameTimerModel,
        ITouchInputModel touchInputModel,
        IPressModel pressModel
    )
    {
        _miniGameTimerModel = miniGameTimerModel;
        _touchInputModel = touchInputModel;
        _pressModel = pressModel;
    }
    
    public IMiniGameModel CreateMiniGameBasedOnType (MiniGameType type)
    {
        switch (type)
        {
            case MiniGameType.TapObjects:
                ITapObjectsMiniGameModel tapObjectsMiniGameModel = new TapObjectsMiniGameModel(
                    _miniGameTimerModel,
                    _pressModel
                );
                return tapObjectsMiniGameModel;
            case MiniGameType.DragObjects:
                return null;
            case MiniGameType.ThrowObjects:
                IThrowObjectsMiniGameModel throwObjectsMiniGameModel = new ThrowObjectsMiniGameModel(
                    _miniGameTimerModel,
                    _touchInputModel
                );
                return throwObjectsMiniGameModel;
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