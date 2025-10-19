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
                return new TapDestroyMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _pressModel
                );
            case MiniGameType.DragSort:
                return new DragSortMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _dragModel
                );
            case MiniGameType.SwipeThrow:
                return new SwipeThrowMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _cameraProvider,
                    _touchInputModel
                );
            case MiniGameType.MoveFind:
                return new MoveFindMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _cameraMoveModel,
                    _touchInputModel,
                    _options.MoveFindMiniGameOptions
                );
            case MiniGameType.ButtonMash:
                return new ButtonMashMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel
                );
            case MiniGameType.JoystickRotate:
                return new JoystickRotateMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel
                );
            case MiniGameType.TapFloating:
                return new TapFloatingMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _pressModel
                );
            case MiniGameType.TapMoving:
                return new TapMovingMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel,
                    _pressModel
                );
            case MiniGameType.ButtonStopwatch:
                return new ButtonStopwatchMiniGameModel(
                    _miniGameSettings,
                    _miniGameTimerModel
                );
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}