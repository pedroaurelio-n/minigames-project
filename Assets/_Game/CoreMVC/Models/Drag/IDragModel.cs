using System;

public interface IDragModel : IDisposable
{
    bool IsDragging { get; }
    IDraggable CurrentDraggable { get; }
    
    void Initialize ();
}