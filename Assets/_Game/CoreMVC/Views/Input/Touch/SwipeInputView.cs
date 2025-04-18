using System;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInputView : MonoBehaviour
{
    public event Action<Touch> OnSwipeBegan;
    public event Action<Touch> OnSwipeEnded;
    
    void Update ()
    {
        List<Touch> touches = TouchUtils.GetTouches();
        if (touches.Count == 0)
            return;

        Touch touch = touches[0];

        switch (touch.phase)
        {
            case TouchPhase.Began:
                OnSwipeBegan?.Invoke(touch);
                break;
            
            case TouchPhase.Ended:
                OnSwipeEnded?.Invoke(touch);
                break;
        }
    }
}