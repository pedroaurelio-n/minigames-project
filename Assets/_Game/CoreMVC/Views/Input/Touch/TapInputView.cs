using System;
using UnityEngine;

public class TapInputView : MonoBehaviour
{
    public event Action<Touch> OnTapBegan;
    public event Action<Touch> OnTapEnded;
    
    void Update ()
    {
        if (TouchUtils.GetTouchCount() == 0)
            return;

        Touch touch = TouchUtils.GetFirstTouch();

        switch (touch.phase)
        {
            case TouchPhase.Began:
                OnTapBegan(touch);
                break;
            
            case TouchPhase.Ended:
                OnTapEnded(touch);
                break;
        }
    }
}