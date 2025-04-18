using System.Collections.Generic;
using UnityEngine;

public static class TouchUtils
{
    static Vector3 _lastMousePosition;
    static Vector3 _lastSimulatedTouch1Position;
    static Vector3 _lastSimulatedTouch2Position;
    static float _simulatedPinchDistance;
    
    public static List<Touch> GetTouches ()
    {
#if UNITY_EDITOR
        List<Touch> touches = new();

        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            Vector3 currentMousePos = Input.mousePosition;

            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                if (Input.mouseScrollDelta.y != 0)
                    _simulatedPinchDistance += Input.mouseScrollDelta.y * 50f;
                Vector3 offset = new(_simulatedPinchDistance * 0.5f, 0, 0);

                Vector3 currentSimulatedTouch1Position = currentMousePos - offset;
                Vector3 currentSimulatedTouch2Position = currentMousePos + offset;

                Touch simulatedTouch1 = CreateTouch(0, currentSimulatedTouch1Position, _lastSimulatedTouch1Position);
                Touch simulatedTouch2 = CreateTouch(1, currentSimulatedTouch2Position, _lastSimulatedTouch2Position);

                touches.Add(simulatedTouch1);
                touches.Add(simulatedTouch2);

                _lastSimulatedTouch1Position = currentSimulatedTouch1Position;
                _lastSimulatedTouch2Position = currentSimulatedTouch2Position;
            }
            else
            {
                Touch singleTouch = CreateTouch(0, currentMousePos, _lastMousePosition);
                touches.Add(singleTouch);
            }

            _lastMousePosition = currentMousePos;
        }

        return touches;
#else
        return Input.touchCount > 0 ? new List<Touch>(Input.touches) : new List<Touch>();
#endif
    }

    static Touch CreateTouch(int fingerId, Vector3 currentPos, Vector3 lastPos)
    {
        TouchPhase phase = TouchPhase.Moved;
        if (Input.GetMouseButtonDown(0)) phase = TouchPhase.Began;
        if (Input.GetMouseButtonUp(0)) phase = TouchPhase.Ended;
        
        return new Touch
        {
            fingerId = fingerId,
            position = currentPos,
            deltaPosition = currentPos - lastPos,
            phase = phase,
            tapCount = 1
        };
    }
}