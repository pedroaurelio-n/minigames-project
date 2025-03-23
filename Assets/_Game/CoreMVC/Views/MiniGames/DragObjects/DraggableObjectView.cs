using UnityEngine;

public class DraggableObjectView : MonoBehaviour, IDraggable
{
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
        rigidbody.MovePosition(worldPosition);
    }

    public void OnDragEnded ()
    {
        meshRenderer.material = _defaultMaterial;
        rigidbody.isKinematic = false;
    }
}
