using System;
using UnityEngine;

public class SwipeInputView : MonoBehaviour
{
    public event Action<Touch> OnSwipeBegan;
    public event Action<Touch> OnSwipeEnded;
    
    void Update ()
    {
        if (TouchUtils.GetTouchCount() == 0)
            return;

        Touch touch = TouchUtils.GetFirstTouch();

        switch (touch.phase)
        {
            case TouchPhase.Began:
                OnSwipeBegan(touch);
                break;
            
            case TouchPhase.Ended:
                OnSwipeEnded(touch);
                break;
        }
    }
}