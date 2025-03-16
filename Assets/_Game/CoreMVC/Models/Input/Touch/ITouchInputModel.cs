using UnityEngine;

public interface ITouchInputModel
{
    bool LongPressStarted { get; }
    
    void SetMainCamera (Camera mainCamera);
    void PerformTap (Vector2 position);
    void PerformSwipe (Vector2 swipeDelta, float diagonalThreshold);
    void StartLongPress (Vector2 position);
    void EndLongPress (Vector2 position);
    void CancelLongPress (Vector2 position);
    void StartTwoPointMove (Vector2 middlePosition);
    void PerformTwoPointMove (Vector2 deltaPosition);
}