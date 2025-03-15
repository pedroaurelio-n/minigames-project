using UnityEngine;

public interface ITouchInputModel
{
    void SetMainCamera (Camera mainCamera);
    void PerformTap (Vector2 tapPosition);
    void PerformSwipe (Vector2 swipeDelta, float diagonalThreshold);
}