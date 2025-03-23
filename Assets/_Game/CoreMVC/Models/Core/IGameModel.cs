using System;
using UnityEngine;

public interface IGameModel : IDisposable
{
    // IMouseInputModel MouseInputModel { get; }
    ITouchInputModel TouchInputModel { get; }
    IDragModel DragModel { get; }
    IPressModel PressModel { get; }
    ISceneChangerModel SceneChangerModel { get; }
    IMiniGameTimerModel MiniGameTimerModel { get; }
    IMiniGameModelFactory MiniGameModelFactory { get; }
    IMiniGameManagerModel MiniGameManagerModel { get; }
    
    Camera MainCamera { get; }

    void Initialize ();
    void LateInitialize ();
}