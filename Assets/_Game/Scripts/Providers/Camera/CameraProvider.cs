using UnityEngine;

public class CameraProvider : ICameraProvider
{
    public Camera MainCamera { get; private set; }
    
    public void SetMainCamera(Camera mainCamera)
    {
        MainCamera = mainCamera;
    }
}