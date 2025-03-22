using System;
using UnityEngine;

public class LongPressInputView : MonoBehaviour
{
    public event Action<Touch> OnLongPressBegan;
    public event Action<Touch> OnLongPressUpdated;
    public event Action<Touch> OnLongPressEnded;
    
    void Update ()
    {
        if (TouchUtils.GetTouchCount() == 0)
            return;

        Touch touch = TouchUtils.GetFirstTouch();

        switch (touch.phase)
        {
            case TouchPhase.Began:
                OnLongPressBegan(touch);
                break;
            
            case TouchPhase.Moved or TouchPhase.Stationary:
                OnLongPressUpdated(touch);
                break;
            
            case TouchPhase.Ended:
                OnLongPressEnded(touch);
                break;
        }
    }
}