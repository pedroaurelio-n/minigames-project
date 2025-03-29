using System;

public interface IGameModel : IDisposable
{
    // IMouseInputModel MouseInputModel { get; }
    ITouchInputModel TouchInputModel { get; }
    IDragModel DragModel { get; }
    IPressModel PressModel { get; }
    ICameraMoveModel CameraMoveModel { get; }
    ISceneChangerModel SceneChangerModel { get; }
    IMiniGameTimerModel MiniGameTimerModel { get; }
    IMiniGameModelFactory MiniGameModelFactory { get; }
    IMiniGameManagerModel MiniGameManagerModel { get; }

    void Initialize ();
    void LateInitialize ();
}