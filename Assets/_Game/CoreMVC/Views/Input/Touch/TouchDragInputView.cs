using System;
using UnityEngine;

public class TouchDragInputView : MonoBehaviour
{
    public event Action<Touch> OnTouchDragUpdated;

    void Update ()
    {
        if (TouchUtils.GetTouchCount() != 1)
            return;

        Touch touch = TouchUtils.GetFirstTouch();
        OnTouchDragUpdated(touch);
    }
}