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
            case MiniGameType.TapDestroy:
                ITapDestroyMiniGameModel tapDestroyMiniGameModel = new TapDestroyMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _pressModel
                );
                return tapDestroyMiniGameModel;
            case MiniGameType.DragSort:
                IDragSortMiniGameModel dragSortMiniGameModel = new DragSortMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _dragModel
                );
                return dragSortMiniGameModel;
            case MiniGameType.SwipeThrow:
                ISwipeThrowMiniGameModel swipeThrowMiniGameModel = new SwipeThrowMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _cameraProvider,
                    _touchInputModel
                );
                return swipeThrowMiniGameModel;
            case MiniGameType.MoveFind:
                IMoveFindMiniGameModel moveFindMiniGameModel = new MoveFindMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _cameraMoveModel,
                    _touchInputModel,
                    _options.MoveFindMiniGameOptions
                    
                );
                return moveFindMiniGameModel;
            case MiniGameType.ButtonMash:
                IButtonMashMiniGameModel buttonMashMiniGameModel = new ButtonMashMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel
                );
                return buttonMashMiniGameModel;
            case MiniGameType.JoystickRotate:
                IJoystickRotateMiniGameModel joystickRotateMiniGameModel = new JoystickRotateMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel
                );
                return joystickRotateMiniGameModel;
            // case MiniGameType.TurnShooter:
            //     return null;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}