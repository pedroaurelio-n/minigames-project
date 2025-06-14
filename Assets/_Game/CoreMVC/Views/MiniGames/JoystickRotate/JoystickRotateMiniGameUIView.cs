using System;
using UnityEngine;

public class JoystickRotateMiniGameUIView : MonoBehaviour
{
    public event Action<Vector2> OnJoystickDirectionUpdated;
    
    [SerializeField] Joystick joystick;

    void Update ()
    {
        OnJoystickDirectionUpdated?.Invoke(joystick.Direction);
    }
}