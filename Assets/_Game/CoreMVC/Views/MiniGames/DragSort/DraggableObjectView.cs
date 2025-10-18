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
    }

    public void OnDragMoved (Vector3 worldPosition)
    {
        Vector3 desiredVelocity = worldPosition - rb.position;
        
        float distance = desiredVelocity.magnitude;
        
        float forceMultiplier = 15f;
        float damping = 1.5f;
        
        Vector3 force = desiredVelocity.normalized * distance * forceMultiplier;
        
        force -= rb.linearVelocity * damping;
        
        float maxForce = 20f;
        if (force.magnitude > maxForce)
        {
            force = force.normalized * maxForce;
        }
        
        rb.AddForce(force, ForceMode.Acceleration);
        
        if (distance < 0.05f)
        {
            rb.position = worldPosition;
        }
    }

    public void OnDragEnded ()
    {
        meshRenderer.material = _defaultMaterial;
    }
}
