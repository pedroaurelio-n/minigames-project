using System;
using UnityEngine;

public class TwoPointMoveInputView : MonoBehaviour
{
    public event Action<Touch, Touch> OnTwoPointMoveUpdated;
    
    void Update ()
    {
        if (TouchProvider.GetTouchCount() < 2)
            return;

        Touch touch1 = TouchProvider.GetFirstTouch();
        Touch touch2 = TouchProvider.GetSecondTouch();

        OnTwoPointMoveUpdated(touch1, touch2);
    }
}