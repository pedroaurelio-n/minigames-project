using System;

public interface ISceneModel : IDisposable
{
    // IMouseInputModel MouseInputModel { get; }
    ITouchInputModel TouchInputModel { get; }
    IDragModel DragModel { get; }
    IPressModel PressModel { get; }
    ICameraMoveModel CameraMoveModel { get; }
    IMiniGameSceneChangerModel MiniGameSceneChangerModel { get; }
    IMiniGameTimerModel MiniGameTimerModel { get; }
    IMiniGameModelFactory MiniGameModelFactory { get; }
    IMiniGameManagerModel MiniGameManagerModel { get; }
    IMiniGameSelectorModel MiniGameSelectorModel { get; }

    void Initialize ();
    void LateInitialize ();
}