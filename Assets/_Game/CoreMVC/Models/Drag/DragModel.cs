using UnityEngine;

public class DragModel : IDragModel
{
    public bool IsDragging { get; private set; }
    public IDraggable CurrentDraggable { get; private set; }
    
    readonly ITouchInputModel _touchInputModel;

    public DragModel (ITouchInputModel touchInputModel)
    {
        _touchInputModel = touchInputModel;
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
    }

    void HandleTouchDragBegan (IDraggable draggable, Vector3 worldPosition)
    {
        IsDragging = true;
        CurrentDraggable = draggable;
        CurrentDraggable.OnDragBegan();
        DebugUtils.Log($"Touch drag began for {draggable.Name}, at {worldPosition}");
    }
    
    void HandleTouchDragMoved (Vector3 worldPosition)
    {
        if (!IsDragging || CurrentDraggable == null)
            return;
        
        CurrentDraggable.OnDragMoved(worldPosition);
        DebugUtils.Log($"Touch drag moved for {CurrentDraggable.Name}, to {worldPosition}");
    }
    
    void HandleTouchDragEnded (Vector3 worldPosition)
    {
        if (!IsDragging || CurrentDraggable == null)
            return;
        
        DebugUtils.Log($"Touch drag ended for {CurrentDraggable.Name}, at {worldPosition}");
        CurrentDraggable.OnDragEnded();
        CurrentDraggable = null;
        IsDragging = false;
    }
}