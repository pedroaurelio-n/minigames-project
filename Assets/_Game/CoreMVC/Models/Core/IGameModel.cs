using UnityEngine;

public interface IGameModel
{
    // IMouseInputModel MouseInputModel { get; }
    ITouchInputModel TouchInputModel { get; }
    ISceneChangerModel SceneChangerModel { get; }
    
    Camera MainCamera { get; }

    void Initialize ();
}