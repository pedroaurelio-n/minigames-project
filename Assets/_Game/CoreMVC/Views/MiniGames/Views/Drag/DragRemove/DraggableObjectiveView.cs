using System;
using UnityEngine;

public class DraggableObjectiveView : PoolableView, IDraggable
{
    public event Action OnObjectiveDragBegan;
    
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material dragMaterial;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Rigidbody rb;

    public string Name => gameObject.name;
    
    Vector3 _lastDragPosition;

    public void Setup (Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        meshRenderer.material = defaultMaterial;
    }

    public void OnDragBegan (Vector3 worldPosition)
    {
        _lastDragPosition = worldPosition;
        meshRenderer.material = dragMaterial;
        OnObjectiveDragBegan?.Invoke();
    }

    public void OnDragMoved (Vector3 worldPosition)
    {
        Vector3 delta = worldPosition - _lastDragPosition;
        transform.position += delta;
        _lastDragPosition = worldPosition;
    }

    public void OnDragEnded (Vector3 worldPosition)
    {
        _lastDragPosition = Vector3.zero;
        meshRenderer.material = defaultMaterial;
    }
}