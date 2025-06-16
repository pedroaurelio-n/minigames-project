using System;
using UnityEngine;

public class DraggableContainerView : MonoBehaviour
{
    public event Action<IDraggable, DraggableContainerView> OnDraggableEnter;
    public event Action<IDraggable, DraggableContainerView> OnDraggableExit;
    
    [field: SerializeField] public DraggableObjectColor Color { get; private set; }
    
    public int ValidObjectsCount { get; set; }

    void OnTriggerEnter (Collider other)
    {
        if (!other.TryGetComponent<IDraggable>(out IDraggable draggable))
            return;
        OnDraggableEnter?.Invoke(draggable, this);
    }

    void OnTriggerExit (Collider other)
    {
        if (!other.TryGetComponent<IDraggable>(out IDraggable draggable))
            return;
        OnDraggableExit?.Invoke(draggable, this);
    }
}