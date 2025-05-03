using System;

public class MiniGameModelFactory : IMiniGameModelFactory
{
    readonly IMiniGameSettings _miniGameSettings;
    readonly IMiniGameTimerModel _miniGameTimerModel;
    readonly ICameraProvider _cameraProvider;
    readonly ITouchInputModel _touchInputModel;
    readonly IPressModel _pressModel;
    readonly IDragModel _dragModel;

    public MiniGameModelFactory (
        IMiniGameSettings miniGameSettings,
        IMiniGameTimerModel miniGameTimerModel,
        ICameraProvider cameraProvider,
        ITouchInputModel touchInputModel,
        IPressModel pressModel,
        IDragModel dragModel
    )
    {
        _miniGameSettings = miniGameSettings;
        _miniGameTimerModel = miniGameTimerModel;
        _cameraProvider = cameraProvider;
        _touchInputModel = touchInputModel;
        _pressModel = pressModel;
        _dragModel = dragModel;
    }
    
    public IMiniGameModel CreateMiniGameBasedOnType (MiniGameType type)
    {
        switch (type)
        {
            case MiniGameType.TapObjects:
                ITapObjectsMiniGameModel tapObjectsMiniGameModel = new TapObjectsMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _pressModel
                );
                return tapObjectsMiniGameModel;
            case MiniGameType.DragObjects:
                IDragObjectsMiniGameModel dragObjectsMiniGameModel = new DragObjectsMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _dragModel
                );
                return dragObjectsMiniGameModel;
            case MiniGameType.ThrowObjects:
                IThrowObjectsMiniGameModel throwObjectsMiniGameModel = new ThrowObjectsMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _cameraProvider,
                    _touchInputModel
                );
                return throwObjectsMiniGameModel;
            case MiniGameType.FindObject:
                IFindObjectMiniGameModel findObjectMiniGameModel = new FindObjectMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _cameraProvider
                );
                return findObjectMiniGameModel;
            case MiniGameType.ClickMilestone:
                IClickMilestoneMiniGameModel clickMilestoneMiniGameModel = new ClickMilestoneMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel
                );
                return clickMilestoneMiniGameModel;
            // case MiniGameType.LongPressObjects:
            //     return null;
            // case MiniGameType.TurnShooter:
            //     return null;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}