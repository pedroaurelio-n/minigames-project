using System;
using UnityEngine;

public interface IGameModel : IDisposable
{
    // IMouseInputModel MouseInputModel { get; }
    ITouchInputModel TouchInputModel { get; }
    ISceneChangerModel SceneChangerModel { get; }
    
    Camera MainCamera { get; }

    void Initialize ();
}