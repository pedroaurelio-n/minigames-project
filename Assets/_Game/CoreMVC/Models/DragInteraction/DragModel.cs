using UnityEngine;

public class DragModel : IDragModel
{
    public bool IsDragging { get; private set; }
    public IDraggable CurrentDraggable { get; private set; }
    
    readonly ITouchInputModel _touchInputModel;
    readonly IPhysicsProvider _physicsProvider;
    readonly LayerMaskOptions _layerMaskOptions;

    Collider _currentDraggableCollider;

    public DragModel (
        ITouchInputModel touchInputModel,
        IPhysicsProvider physicsProvider,
        LayerMaskOptions layerMaskOptions
    )
    {
        _touchInputModel = touchInputModel;
        _physicsProvider = physicsProvider;
        _layerMaskOptions = layerMaskOptions;
    }
    
    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        _touchInputModel.OnTouchDragBegan += HandleTouchDragBegan;
        _touchInputModel.OnTouchDragMoved += HandleTouchDragMoved;
        _touchInputModel.OnTouchDragEnded += HandleTouchDragEnded;
    }

    void RemoveListeners ()
    {
        _touchInputModel.OnTouchDragBegan -= HandleTouchDragBegan;
        _touchInputModel.OnTouchDragMoved -= HandleTouchDragMoved;
        _touchInputModel.OnTouchDragEnded -= HandleTouchDragEnded;
    }

    void HandleTouchDragBegan (Vector2 touchPosition)
    {
        if (_touchInputModel.MainCamera == null)
            return;
        
        Ray ray = _touchInputModel.MainCamera.ScreenPointToRay(touchPosition);

        if (!_physicsProvider.Raycast(ray, _layerMaskOptions.InteractableLayers, out RaycastHit hit))
            return;
        
        if (hit.collider == _currentDraggableCollider)
            return;

        _currentDraggableCollider = hit.collider;
            
        IDraggable draggable = _currentDraggableCollider.GetComponent<IDraggable>();
            
        if (draggable == null)
            return;

        Vector3 worldPosition = GetCorrectedWorldPosition(touchPosition);
        IsDragging = true;
        CurrentDraggable = draggable;
        CurrentDraggable.OnDragBegan();
        
        DebugUtils.Log($"Touch drag began for {draggable.Name}, at {worldPosition}");
    }
    
    void HandleTouchDragMoved (Vector2 touchPosition)
    {
        if (!IsDragging || CurrentDraggable == null)
            return;
        
        Vector3 worldPosition = GetCorrectedWorldPosition(touchPosition);
        CurrentDraggable.OnDragMoved(worldPosition);
        
        DebugUtils.Log($"Touch drag moved for {CurrentDraggable.Name}, to {worldPosition}");
    }
    
    void HandleTouchDragEnded (Vector2 touchPosition)
    {
        if (!IsDragging || CurrentDraggable == null || _currentDraggableCollider == null)
            return;
        
        Vector3 worldPosition = GetCorrectedWorldPosition(touchPosition);
        DebugUtils.Log($"Touch drag ended for {CurrentDraggable.Name}, at {worldPosition}");
        
        CurrentDraggable.OnDragEnded();
        CurrentDraggable = null;
        _currentDraggableCollider = null;
        IsDragging = false;
    }

    Vector3 GetCorrectedWorldPosition (Vector2 touchPosition)
    {
        float depth = Mathf.Abs(_touchInputModel.MainCamera.transform.position.z);
        Vector3 touchCorrectedPosition = new(touchPosition.x, touchPosition.y, depth);
        return _touchInputModel.MainCamera.ScreenToWorldPoint(touchCorrectedPosition);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}