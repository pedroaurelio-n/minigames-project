using System;
using UnityEngine;

public class TwoPointZoomInputView : MonoBehaviour
{
    public event Action<Touch, Touch> OnTwoPointZoomUpdated;
    
    void Update ()
    {
        if (TouchUtils.GetTouchCount() != 2)
            return;

        Touch touch1 = TouchUtils.GetFirstTouch();
        Touch touch2 = TouchUtils.GetSecondTouch();

        OnTwoPointZoomUpdated(touch1, touch2);
    }
}
