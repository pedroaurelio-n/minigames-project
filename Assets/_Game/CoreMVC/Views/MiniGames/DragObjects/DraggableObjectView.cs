using UnityEngine;

public class DraggableObjectView : PoolableView, IDraggable
{
    [field: SerializeField] public DraggableObjectColor Color { get; private set; }
    
    [SerializeField] Material[] defaultMaterials;
    [SerializeField] Material[] dragMaterials;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Rigidbody rb;

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
        rb.isKinematic = true;
    }

    public void OnDragMoved (Vector3 worldPosition)
    {
        Vector3 direction = (worldPosition - rb.position).normalized;
        float distance = Vector3.Distance(rb.position, worldPosition);
        
        if (!rb.SweepTest(direction, out RaycastHit hit, distance))
            rb.MovePosition(worldPosition);
    }

    public void OnDragEnded ()
    {
        Vector3 previousVelocity = rb.velocity;
        meshRenderer.material = _defaultMaterial;
        rb.isKinematic = false;
        rb.velocity = previousVelocity * 0.3f;
    }
}
