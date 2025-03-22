using System;
using UnityEngine;

public class TouchDragInputView : MonoBehaviour
{
    public event Action<Touch> OnTouchDragUpdated;

    void Update ()
    {
        if (TouchProvider.GetTouchCount() != 1)
            return;

        Touch touch = TouchProvider.GetFirstTouch();
        OnTouchDragUpdated(touch);
    }
}