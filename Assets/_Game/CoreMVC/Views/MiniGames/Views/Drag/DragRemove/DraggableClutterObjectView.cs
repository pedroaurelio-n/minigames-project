using UnityEngine;

public class DraggableClutterObjectView : PoolableView, IDraggable
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material dragMaterial;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Rigidbody rb;

    public string Name => gameObject.name;

    int _index;
    Vector3 _lastDragPosition;

    public void Setup (int index, Vector3 position, Quaternion rotation)
    {
        _index = index;
        
        position.z = -_index;
        transform.position = position;
        transform.rotation = rotation;

        meshRenderer.material = defaultMaterial;
    }

    public void OnDragBegan (Vector3 worldPosition)
    {
        _lastDragPosition = worldPosition;
        meshRenderer.material = dragMaterial;
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