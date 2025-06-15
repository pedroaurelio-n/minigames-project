using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputView : MonoBehaviour
{
    public event Action<Vector2> OnPositionChanged;
    public event Action OnLeftClick;
    public event Action OnRightClick;

    Vector2 _currentMousePosition;
    
    void Update ()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
            return;
        
        Vector2 newPosition = mouse.position.ReadValue();
        if (_currentMousePosition != newPosition)
        {
            _currentMousePosition = newPosition;
            OnPositionChanged?.Invoke(_currentMousePosition);
        }
        
        if (mouse.leftButton.wasPressedThisFrame)
            OnLeftClick?.Invoke();
        
        if (mouse.rightButton.wasPressedThisFrame)
            OnRightClick?.Invoke();
    }
}