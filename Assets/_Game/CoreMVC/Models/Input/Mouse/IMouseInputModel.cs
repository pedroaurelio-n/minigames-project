using UnityEngine;

public interface IMouseInputModel
{
    Vector2 CurrentPosition { get; }
    bool IsHoveringInteractable { get; }

    void SetMainCamera (Camera mainCamera);
    void UpdatePosition (Vector2 position);
    void LeftClick ();
    void RightClick ();
}