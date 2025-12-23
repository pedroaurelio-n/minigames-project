using System;

public class MiniGameModelFactory : IMiniGameModelFactory
{
    readonly IMiniGameSettings _miniGameSettings;
    readonly IMiniGameDifficultyModel _miniGameDifficultyModel;
    readonly IMiniGameTimerModel _miniGameTimerModel;
    readonly ICameraProvider _cameraProvider;
    readonly ICameraMoveModel _cameraMoveModel;
    readonly ITouchInputModel _touchInputModel;
    readonly IPressModel _pressModel;
    readonly IDragModel _dragModel;
    readonly MiniGameOptions _options;

    public MiniGameModelFactory (
        IMiniGameSettings miniGameSettings,
        IMiniGameDifficultyModel miniGameDifficultyModel,
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
        _miniGameDifficultyModel = miniGameDifficultyModel;
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
            case MiniGameType.ButtonMash:
                return new ButtonMashMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel
                );
            case MiniGameType.ButtonStopwatch:
                return new ButtonStopwatchMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel
                );
            case MiniGameType.DragSort:
                return new DragSortMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel,
                    _dragModel
                );
            case MiniGameType.DragRemove:
                return new DragRemoveMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel,
                    _dragModel
                );
            case MiniGameType.JoystickRotate:
                return new JoystickRotateMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel
                );
            case MiniGameType.JoystickAim:
                return new JoystickAimMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel
                );
            case MiniGameType.LongPressBombs:
                return new LongPressBombsMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel,
                    _pressModel
                );
            case MiniGameType.MoveFind:
                return new MoveFindMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel,
                    _cameraMoveModel,
                    _touchInputModel,
                    _options.MoveFindMiniGameOptions
                );
            case MiniGameType.SwipeThrow:
                return new SwipeThrowMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel,
                    _cameraProvider,
                    _touchInputModel
                );
            case MiniGameType.TapDestroy:
                return new TapDestroyMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel,
                    _pressModel
                );
            case MiniGameType.TapFloating:
                return new TapFloatingMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel,
                    _pressModel
                );
            case MiniGameType.TapMoving:
                return new TapMovingMiniGameModel(
                    _miniGameSettings,
                    _miniGameDifficultyModel,
                    _miniGameTimerModel,
                    _pressModel
                );
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}