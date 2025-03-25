using UnityEngine;

public class DraggableObjectView : MonoBehaviour, IDraggable
{
    [field: SerializeField] public DraggableObjectColor Color { get; private set; }
    
    [SerializeField] Material dragMaterial;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Rigidbody rigidbody;

    public string Name => gameObject.name;

    Material _defaultMaterial;

    void Awake ()
    {
        _defaultMaterial = meshRenderer.material;
    }

    public void OnDragBegan ()
    {
        meshRenderer.material = dragMaterial;
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
