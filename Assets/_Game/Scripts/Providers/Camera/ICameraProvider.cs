using UnityEngine;

public interface ICameraProvider
{
    Camera MainCamera { get; }
    
    void SetMainCamera(Camera mainCamera);
}