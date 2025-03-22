using System;
using UnityEngine;

public class TwoPointMoveInputView : MonoBehaviour
{
    public event Action<Touch, Touch> OnTwoPointMoveUpdated;
    
    void Update ()
    {
        if (TouchUtils.GetTouchCount() != 2)
            return;

        Touch touch1 = TouchUtils.GetFirstTouch();
        Touch touch2 = TouchUtils.GetSecondTouch();

        OnTwoPointMoveUpdated(touch1, touch2);
    }
}