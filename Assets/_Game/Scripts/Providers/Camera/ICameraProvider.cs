using UnityEngine;

public interface ICameraProvider
{
    Camera MainCamera { get; }
    
    void SetMainCamera(Camera mainCamera);
    bool IsContainedInCameraBounds (Renderer renderer, float margin = 0.05f);
}