using System;
using System.Collections.Generic;
using UnityEngine;

public class TapInputView : MonoBehaviour
{
    public event Action<Touch> OnTapBegan;
    public event Action<Touch> OnTapEnded;
    
    void Update ()
    {
        List<Touch> touches = TouchUtils.GetTouches();
        if (touches.Count == 0)
            return;

        Touch touch = touches[0];

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