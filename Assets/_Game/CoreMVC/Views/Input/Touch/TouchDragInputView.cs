using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchDragInputView : MonoBehaviour
{
    public event Action<Touch> OnTouchDragUpdated;

    void Update ()
    {
        List<Touch> touches = TouchUtils.GetTouches();
        if (touches.Count != 1)
            return;

        Touch touch = touches[0];
        OnTouchDragUpdated?.Invoke(touch);
    }
}