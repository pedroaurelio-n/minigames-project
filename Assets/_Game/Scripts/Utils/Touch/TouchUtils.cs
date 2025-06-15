using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

using Touch = UnityEngine.Touch;
using TouchPhase = UnityEngine.TouchPhase;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using EnhancedTouchPhase = UnityEngine.InputSystem.TouchPhase;

public static class TouchUtils
{
    static Vector3 _lastMousePosition;
    static Vector3 _lastSimulatedTouch1Position;
    static Vector3 _lastSimulatedTouch2Position;
    static float _simulatedPinchDistance;
    
    public static List<Touch> GetTouches ()
    {
#if UNITY_EDITOR
        return GetEditorSimulatedTouches();
#else
        return GetRuntimeTrackedTouches();
#endif
    }

    static List<Touch> GetEditorSimulatedTouches ()
    {
        List<Touch> touches = new();
        
        Mouse mouse = Mouse.current;
        Keyboard keyboard = Keyboard.current;
        if (mouse == null || keyboard == null)
            return touches;
        
        bool isPressed = mouse.leftButton.isPressed;
        bool wasPressedThisFrame = mouse.leftButton.wasPressedThisFrame;
        bool wasReleasedThisFrame = mouse.leftButton.wasReleasedThisFrame;
        
        if (isPressed || wasPressedThisFrame || wasReleasedThisFrame)
        {
            Vector3 currentMousePos = mouse.position.ReadValue();
            
            bool altPressed = keyboard.leftAltKey.isPressed || keyboard.rightAltKey.isPressed;
        
            if (altPressed)
            {
                float scrollDeltaY = mouse.scroll.ReadValue().y;
                if (scrollDeltaY != 0)
                    _simulatedPinchDistance += scrollDeltaY * 50f;
                
                Vector3 offset = new(_simulatedPinchDistance * 0.5f, 0, 0);
        
                Vector3 currentSimulatedTouch1Position = currentMousePos - offset;
                Vector3 currentSimulatedTouch2Position = currentMousePos + offset;

                Touch simulatedTouch1 = CreateTouch(
                    0,
                    currentSimulatedTouch1Position,
                    _lastSimulatedTouch1Position,
                    wasPressedThisFrame,
                    wasReleasedThisFrame
                );
                Touch simulatedTouch2 = CreateTouch(
                    1,
                    currentSimulatedTouch2Position,
                    _lastSimulatedTouch2Position,
                    wasPressedThisFrame,
                    wasReleasedThisFrame
                );
        
                touches.Add(simulatedTouch1);
                touches.Add(simulatedTouch2);
        
                _lastSimulatedTouch1Position = currentSimulatedTouch1Position;
                _lastSimulatedTouch2Position = currentSimulatedTouch2Position;
            }
            else
            {
                Touch singleTouch = CreateTouch(
                    0,
                    currentMousePos,
                    _lastMousePosition,
                    wasPressedThisFrame,
                    wasReleasedThisFrame
                );
                touches.Add(singleTouch);
            }
        
            _lastMousePosition = currentMousePos;
        }

        return touches;
    }

    static List<Touch> GetRuntimeTrackedTouches ()
    {
        List<Touch> touches = new();
        ReadOnlyArray<EnhancedTouch> activeTouches = EnhancedTouch.activeTouches;

        foreach (EnhancedTouch touch in activeTouches)
            touches.Add(CreateTouch(touch));
        
        return touches;
    }

    static Touch CreateTouch (int fingerId, Vector3 currentPos, Vector3 lastPos, bool wasPressedThisFrame, bool wasReleasedThisFrame)
    {
        TouchPhase phase = TouchPhase.Moved;
        if (wasPressedThisFrame)
            phase = TouchPhase.Began;
        else if (wasReleasedThisFrame)
            phase = TouchPhase.Ended;
        
        return new Touch
        {
            fingerId = fingerId,
            position = currentPos,
            deltaPosition = currentPos - lastPos,
            phase = phase,
            tapCount = 1
        };
    }
    
    static Touch CreateTouch (EnhancedTouch touch)
    {
        TouchPhase phase = touch.phase switch
        {
            EnhancedTouchPhase.None => TouchPhase.Ended,
            EnhancedTouchPhase.Began => TouchPhase.Began,
            EnhancedTouchPhase.Moved => TouchPhase.Moved,
            EnhancedTouchPhase.Ended => TouchPhase.Ended,
            EnhancedTouchPhase.Canceled => TouchPhase.Canceled,
            EnhancedTouchPhase.Stationary => TouchPhase.Stationary,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        return new Touch
        {
            fingerId = touch.finger.index,
            position = touch.screenPosition,
            deltaPosition = touch.delta,
            phase = phase,
            tapCount = touch.tapCount
        };
    }
}