using System;

public class MiniGameModelFactory : IMiniGameModelFactory
{
    readonly IMiniGameSettings _miniGameSettings;
    readonly IMiniGameTimerModel _miniGameTimerModel;
    readonly ICameraProvider _cameraProvider;
    readonly ICameraMoveModel _cameraMoveModel;
    readonly ITouchInputModel _touchInputModel;
    readonly IPressModel _pressModel;
    readonly IDragModel _dragModel;
    readonly MiniGameOptions _options;

    public MiniGameModelFactory (
        IMiniGameSettings miniGameSettings,
        IMiniGameTimerModel miniGameTimerModel,
        ICameraProvider cameraProvider,
        ICameraMoveModel cameraMoveModel,
        ITouchInputModel touchInputModel,
        IPressModel pressModel,
        IDragModel dragModel,
        MiniGameOptions options
    )
    {
        _miniGameSettings = miniGameSettings;
        _miniGameTimerModel = miniGameTimerModel;
        _cameraProvider = cameraProvider;
        _cameraMoveModel = cameraMoveModel;
        _touchInputModel = touchInputModel;
        _pressModel = pressModel;
        _dragModel = dragModel;
        _options = options;
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
                    _cameraMoveModel,
                    _touchInputModel,
                    _options.FindObjectMiniGameOptions
                    
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