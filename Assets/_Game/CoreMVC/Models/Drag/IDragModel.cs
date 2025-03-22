public interface IDragModel
{
    bool IsDragging { get; }
    IDraggable CurrentDraggable { get; }
    
    void Initialize ();
}