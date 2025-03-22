using System.Collections.Generic;
using UnityEngine;

public static class TouchUtils
{
    static Vector3 _lastMousePosition;
    
    public static List<Touch> GetTouches ()
    {
#if UNITY_EDITOR
        List<Touch> touches = new List<Touch>();

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Touch fakeTouch = new Touch
            {
                fingerId = 0,
                position = Input.mousePosition,
                deltaPosition = Input.mousePosition - _lastMousePosition,
                phase = GetTouchPhase(),
                tapCount = 1
            };
            
            touches.Add(fakeTouch);
            _lastMousePosition = Input.mousePosition;
        }

        return touches;
#else
        if (Input.touchCount == 0)
            return new List<Touch>();

        Touch touch = Input.GetTouch(0);
        return new List<Touch>(Input.touches);
#endif
    }

    public static Touch GetFirstTouch ()
    {
#if UNITY_EDITOR
        return GetTouches()[0];
#else
        return Input.GetTouch(0);
#endif
    }
    
    public static Touch GetSecondTouch ()
    {
#if UNITY_EDITOR
        return GetTouches()[1];
#else
        return Input.GetTouch(1);
#endif
    }
    
    public static int GetTouchCount ()
    {
#if UNITY_EDITOR
        return GetTouches().Count;
#else
        return Input.touchCount;
#endif
    }

    static TouchPhase GetTouchPhase ()
    {
        if (Input.GetMouseButtonDown(0))
            return TouchPhase.Began;
        if (Input.GetMouseButtonUp(0))
            return TouchPhase.Ended;
        return TouchPhase.Moved;
    }
}