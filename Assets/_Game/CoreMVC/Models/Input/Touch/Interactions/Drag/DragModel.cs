using UnityEngine;

public class DragModel : IDragModel
{
    public bool IsDragging { get; private set; }
    public IDraggable CurrentDraggable { get; private set; }
    
    readonly ICameraProvider _cameraProvider;
    readonly ITouchInputModel _touchInputModel;
    readonly IInputRaycastProvider _inputRaycastProvider;
    readonly LayerMaskOptions _layerMaskOptions;

    Collider _currentDraggableCollider;

    public DragModel (
        ICameraProvider cameraProvider,
        ITouchInputModel touchInputModel,
        IInputRaycastProvider inputRaycastProvider,
        LayerMaskOptions layerMaskOptions
    )
    {
        _cameraProvider = cameraProvider;
        _touchInputModel = touchInputModel;
        _inputRaycastProvider = inputRaycastProvider;
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
        if (!_inputRaycastProvider.TryGetRaycastHit(touchPosition,
                _layerMaskOptions.InteractableLayers,
                out RaycastHit hit))
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
        CurrentDraggable.OnDragBegan(worldPosition);
        
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
        
        CurrentDraggable.OnDragEnded(worldPosition);
        CurrentDraggable = null;
        _currentDraggableCollider = null;
        IsDragging = false;
    }

    Vector3 GetCorrectedWorldPosition (Vector2 touchPosition)
    {
        float depth = Mathf.Abs(_cameraProvider.MainCamera.transform.position.z);
        Vector3 touchCorrectedPosition = new(touchPosition.x, touchPosition.y, depth);
        return _cameraProvider.MainCamera.ScreenToWorldPoint(touchCorrectedPosition);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}