using System;
using UnityEngine;

public class JoystickAimMiniGameUIView : MiniGameUIView
{
    public event Action<Vector2> OnJoystickDirectionUpdated;
    
    [SerializeField] Joystick joystick;

    public void UpdateJoystick ()
    {
        OnJoystickDirectionUpdated?.Invoke(joystick.Direction);
    }
}