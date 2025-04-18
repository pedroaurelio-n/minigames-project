using System;
using System.Collections.Generic;
using UnityEngine;

public class LongPressInputView : MonoBehaviour
{
    public event Action<Touch> OnLongPressBegan;
    public event Action<Touch> OnLongPressUpdated;
    public event Action<Touch> OnLongPressEnded;
    
    void Update ()
    {
        List<Touch> touches = TouchUtils.GetTouches();
        if (touches.Count == 0)
            return;

        Touch touch = touches[0];

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