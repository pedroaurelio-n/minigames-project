using UnityEngine;

public interface IDraggable
{
    string Name { get; }
    
    void OnDragBegan (Vector3 worldPosition);
    void OnDragMoved (Vector3 worldPosition);
    void OnDragEnded (Vector3 worldPosition);
}