using UnityEngine;

public class InputRaycastProvider : IInputRaycastProvider
{
    readonly ICameraProvider _cameraProvider;
    readonly IPhysicsProvider _physicsProvider;

    public InputRaycastProvider (
        ICameraProvider cameraProvider,
        IPhysicsProvider physicsProvider
    )
    {
        _cameraProvider = cameraProvider;
        _physicsProvider = physicsProvider;
    }
    
    public bool TryGetRaycastHit (Vector2 touchPosition, LayerMask layerMask, out RaycastHit hit)
    {
        hit = default;
        
        if (_cameraProvider.MainCamera == null)
            return false;
        
        Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(touchPosition);

        return _physicsProvider.Raycast(ray, layerMask, out hit);
    }

    public bool TryGetHitComponent<T> (Vector2 touchPosition, LayerMask layerMask, out T component) where T : class
    {
        component = null;
        
        if (!TryGetRaycastHit(touchPosition, layerMask, out RaycastHit hit))
            return false;
        
        component = hit.collider.gameObject.GetComponent<T>() ?? hit.collider.GetComponentInParent<T>();
        return component != null;
    }
}