using UnityEngine;

public interface IGameModel
{
    IMouseInputModel MouseInputModel { get; }
    ISceneChangerModel SceneChangerModel { get; }
    
    Camera MainCamera { get; }

    void Initialize ();
}