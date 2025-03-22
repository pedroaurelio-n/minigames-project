using UnityEngine;

public interface IDraggable
{
    string Name { get; }
    
    void OnDragBegan ();
    void OnDragMoved (Vector3 worldPosition);
    void OnDragEnded ();
}