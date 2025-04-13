using UnityEngine;

public class DraggableObjectView : PoolableView, IDraggable
{
    [field: SerializeField] public DraggableObjectColor Color { get; private set; }
    
    [SerializeField] Material[] defaultMaterials;
    [SerializeField] Material[] dragMaterials;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Rigidbody rigidbody;

    public string Name => gameObject.name;
    
    Material _defaultMaterial;
    Material _dragMaterial;

    public void Setup (DraggableObjectColor randomColor)
    {
        Color = randomColor;
        _defaultMaterial = defaultMaterials[(int)randomColor];
        _dragMaterial = dragMaterials[(int)randomColor];

        meshRenderer.material = _defaultMaterial;
    }

    public void OnDragBegan ()
    {
        meshRenderer.material = _dragMaterial;
        rigidbody.isKinematic = true;
    }

    public void OnDragMoved (Vector3 worldPosition)
    {
        Vector3 direction = (worldPosition - rigidbody.position).normalized;
        float distance = Vector3.Distance(rigidbody.position, worldPosition);
        
        if (!rigidbody.SweepTest(direction, out RaycastHit hit, distance))
            rigidbody.MovePosition(worldPosition);
    }

    public void OnDragEnded ()
    {
        Vector3 previousVelocity = rigidbody.velocity;
        meshRenderer.material = _defaultMaterial;
        rigidbody.isKinematic = false;
        rigidbody.velocity = previousVelocity * 0.3f;
    }
}
