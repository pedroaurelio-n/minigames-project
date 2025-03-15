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

    public void PerformSwipe (Vector2 swipeDelta, float diagonalThreshold)
    {
        float x = swipeDelta.x;
        float y = swipeDelta.y;
        float absX = Mathf.Abs(x);
        float absY = Mathf.Abs(y);

        if (absX > absY + diagonalThreshold * absY)
            Debug.Log(x > 0 ? "Right swipe" : "Left swipe");
        else if (absY > absX + diagonalThreshold * absX)
            Debug.Log(y > 0 ? "Up swipe" : "Down swipe");
        else
        {
            Debug.Log(
                x > 0
                    ? y > 0
                        ? "Up Right swipe"
                        : "Down Right swipe"
                    : y > 0
                        ? "Up Left swipe"
                        : "Down Left swipe"
            );
        }
    }
}