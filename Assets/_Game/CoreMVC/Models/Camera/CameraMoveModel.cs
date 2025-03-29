using UnityEngine;

public class CameraMoveModel : ICameraMoveModel
{
    //TODO pedro: possibly move logic to FindObjectMiniGameModel
    const float ZOOM_SPEED = 0.001f;
    const float MIN_CAMERA_ZOOM = 50f;
    const float MAX_CAMERA_ZOOM = 180f;
    
    const float ROTATION_SPEED = 100f;
    
    readonly ICameraProvider _cameraProvider;
    readonly ITouchInputModel _touchInputModel;
    
    public CameraMoveModel(
        ICameraProvider cameraProvider,
        ITouchInputModel touchInputModel
    )
    {
        _cameraProvider = cameraProvider;
        _touchInputModel = touchInputModel;
    }

    public void Initialize()
    {
        AddListeners();
    }

    void AddListeners()
    {
        _touchInputModel.OnTwoPointZoomPerformed += HandleTwoPointZoomPerformed;
        _touchInputModel.OnTwoPointMoveStarted += HandleTwoPointMoveStarted;
        _touchInputModel.OnTwoPointMovePerformed += HandleTwoPointMovePerformed;
    }
    
    void RemoveListeners()
    {
        _touchInputModel.OnTwoPointZoomPerformed -= HandleTwoPointZoomPerformed;
        _touchInputModel.OnTwoPointMoveStarted -= HandleTwoPointMoveStarted;
        _touchInputModel.OnTwoPointMovePerformed -= HandleTwoPointMovePerformed;
    }

    void HandleTwoPointZoomPerformed(float difference)
    {
        float newFov = (ZOOM_SPEED * difference) / Screen.dpi;
        _cameraProvider.MainCamera.fieldOfView += newFov;
        _cameraProvider.MainCamera.fieldOfView = Mathf.Clamp(_cameraProvider.MainCamera.fieldOfView, MIN_CAMERA_ZOOM, MAX_CAMERA_ZOOM);
    }

    void HandleTwoPointMoveStarted(Vector2 middlePosition)
    {
    }

    void HandleTwoPointMovePerformed(Vector2 deltaPosition)
    {
        float yaw = -deltaPosition.x * ROTATION_SPEED;
        float pitch = deltaPosition.y * ROTATION_SPEED;
        
        Vector3 currentEuler = _cameraProvider.MainCamera.transform.eulerAngles;
        currentEuler.y += yaw;
        currentEuler.x += pitch;
        _cameraProvider.MainCamera.transform.eulerAngles = currentEuler;
    }
    
    public void Dispose()
    {
        RemoveListeners();
    }
}