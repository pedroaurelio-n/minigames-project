using System;
using System.Collections.Generic;
using UnityEngine;

public class TwoPointZoomInputView : MonoBehaviour
{
    public event Action<Touch, Touch> OnTwoPointZoomUpdated;
    
    void Update ()
    {
        List<Touch> touches = TouchUtils.GetTouches();
        if (touches.Count != 2)
            return;

        Touch touch1 = touches[0];
        Touch touch2 = touches[1];

        OnTwoPointZoomUpdated?.Invoke(touch1, touch2);
    }
}
