using UnityEngine;

public class CameraMoveModel : ICameraMoveModel
{
    readonly ICameraProvider _cameraProvider;
    
    public CameraMoveModel(
        ICameraProvider cameraProvider
    )
    {
        _cameraProvider = cameraProvider;
    }

    public void MoveCamera (Vector2 moveVector)
    {
        float yaw = -moveVector.x;
        float pitch = moveVector.y;
        
        Vector3 currentEuler = _cameraProvider.MainCamera.transform.eulerAngles;
        currentEuler.y += yaw;
        currentEuler.x += pitch;
        _cameraProvider.MainCamera.transform.eulerAngles = currentEuler;
    }
    
    public void ZoomCamera (
        float zoomAmount,
        float minZoom = Mathf.NegativeInfinity,
        float maxZoom = Mathf.Infinity
    )
    {
        float newFov = zoomAmount / Screen.dpi;
        _cameraProvider.MainCamera.fieldOfView += newFov;
        _cameraProvider.MainCamera.fieldOfView = Mathf.Clamp(_cameraProvider.MainCamera.fieldOfView, minZoom, maxZoom);
    }
}