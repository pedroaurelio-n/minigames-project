using UnityEngine;

public interface ITouchInputModel
{
    void SetMainCamera (Camera mainCamera);
    void PerformTap (Vector2 tapPosition);
}