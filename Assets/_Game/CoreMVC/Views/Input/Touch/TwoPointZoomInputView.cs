using System;
using UnityEngine;

public class TwoPointZoomInputView : MonoBehaviour
{
    public event Action<Touch, Touch> OnTwoPointZoomUpdated;
    
    void Update ()
    {
        if (TouchProvider.GetTouchCount() != 2)
            return;

        Touch touch1 = TouchProvider.GetFirstTouch();
        Touch touch2 = TouchProvider.GetSecondTouch();

        OnTwoPointZoomUpdated(touch1, touch2);
    }
}
