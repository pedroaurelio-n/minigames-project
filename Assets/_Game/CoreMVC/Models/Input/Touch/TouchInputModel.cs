using UnityEngine;

public class TouchInputModel : ITouchInputModel
{
    readonly IPhysicsProvider _physicsProvider;
    readonly LayerMasksOptions _options;
    
    Camera _mainCamera;
    
    public TouchInputModel (IPhysicsProvider physicsProvider)
    {
        _physicsProvider = physicsProvider;

        _options = GameGlobalOptions.Instance.LayerMasks;
    }
    
    public void SetMainCamera (Camera mainCamera) => _mainCamera = mainCamera;

    public void PerformTap (Vector2 position)
    {
        Debug.Log($"Tap performed at: {position}");
    }
}