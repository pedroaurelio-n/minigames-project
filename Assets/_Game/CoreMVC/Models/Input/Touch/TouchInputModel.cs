using System;
using UnityEngine;

public class TouchInputModel : ITouchInputModel
{
    public event Action<Vector2> OnTapPerformed;
    public event OnSwipePerformedHandler OnSwipePerformed;
    public event Action<Vector2> OnLongPressStarted;
    public event Action<Vector2, float> OnLongPressEnded;
    public event Action<Vector2> OnLongPressCancelled;
    public event Action<Vector2> OnTwoPointMoveStarted;
    public event Action<Vector2> OnTwoPointMovePerformed;
    public event Action<float> OnTwoPointZoomPerformed;
    public event Action<Vector2> OnTouchDragBegan;
    public event Action<Vector2> OnTouchDragMoved;
    public event Action<Vector2> OnTouchDragEnded;
    
    readonly TapInputOptions _tapInputOptions;
    readonly SwipeInputOptions _swipeInputOptions;
    readonly LongPressInputOptions _longPressInputOptions;
    readonly TwoPointMoveInputOptions _twoPointMoveInputOptions;
    readonly TwoPointZoomInputOptions _twoPointZoomInputOptions;
    
    bool _longPressStarted;

    bool _twoPointMoveStarted;
    Vector2 _twoPointMoveStartPosition;

    public TouchInputModel (
        TapInputOptions tapInputOptions,
        SwipeInputOptions swipeInputOptions,
        LongPressInputOptions longPressInputOptions,
        TwoPointMoveInputOptions twoPointMoveInputOptions,
        TwoPointZoomInputOptions twoPointZoomInputOptions
    )
    {
        _tapInputOptions = tapInputOptions;
        _swipeInputOptions = swipeInputOptions;
        _longPressInputOptions = longPressInputOptions;
        _twoPointMoveInputOptions = twoPointMoveInputOptions;
        _twoPointZoomInputOptions = twoPointZoomInputOptions;
    }

    public void PerformTap (Vector2 startPosition, Vector2 endPosition, float duration)
    {
        float distanceSquared = (endPosition - startPosition).sqrMagnitude;
        
        if (duration > _tapInputOptions.MaxTimeThreshold
            || distanceSquared > _tapInputOptions.MaxMovementThreshold * _tapInputOptions.MaxMovementThreshold)
            return;
        
        OnTapPerformed?.Invoke(endPosition);
    }

    public void PerformSwipe (Vector2 startPosition, Vector2 endPosition, float duration)
    {
        Vector2 delta = endPosition - startPosition;
        if (duration > _swipeInputOptions.MaxTimeThreshold
            || delta.sqrMagnitude < _swipeInputOptions.MinMovementThreshold * _swipeInputOptions.MinMovementThreshold)
            return;
        
        float x = delta.x;
        float y = delta.y;
        float absX = Mathf.Abs(x);
        float absY = Mathf.Abs(y);

        OnSwipePerformed?.Invoke(startPosition, endPosition, delta.normalized, delta, duration);
        if (absX > absY + _swipeInputOptions.DiagonalThreshold * absY)
            DebugUtils.Log(x > 0 ? "Right swipe" : "Left swipe");
        else if (absY > absX + _swipeInputOptions.DiagonalThreshold * absX)
            DebugUtils.Log(y > 0 ? "Up swipe" : "Down swipe");
        else
        {
            DebugUtils.Log(
                x > 0
                    ? y > 0
                        ? "Up Right swipe"
                        : "Down Right swipe"
                    : y > 0
                        ? "Up Left swipe"
                        : "Down Left swipe"
            );
        }
    }

    public void EvaluateLongPressUpdate (
        Vector2 startPosition,
        Vector2 currentPosition,
        float startTime,
        ref bool longPressCancelled
    )
    {
        float distanceSquared = (currentPosition - startPosition).sqrMagnitude;

        if (_longPressStarted)
        {
            if (distanceSquared > _longPressInputOptions.MaxMovementCancelThreshold
                * _longPressInputOptions.MaxMovementCancelThreshold)
            {
                CancelLongPress(currentPosition);
                longPressCancelled = true;
            }
            return;
        }
        
        float duration = Time.time - startTime;
        if (duration < _longPressInputOptions.MinTimeThreshold
            || distanceSquared > _longPressInputOptions.MaxMovementStartThreshold
            * _longPressInputOptions.MaxMovementStartThreshold)
            return;

        StartLongPress(currentPosition);
    }

    public void EvaluateLongPressEnd (
        Vector2 startPosition,
        Vector2 endPosition,
        float startTime,
        ref bool longPressCancelled
    )
    {
        if (longPressCancelled)
            return;
        
        float duration = Time.time - startTime;
        if (duration < _longPressInputOptions.MinTimeThreshold)
            return;
        
        if (!_longPressStarted)
        {
            CancelLongPress(endPosition);
            return;
        }

        float distanceSquared = (endPosition - startPosition).sqrMagnitude;
        if (distanceSquared > _longPressInputOptions.MaxMovementCancelThreshold
            * _longPressInputOptions.MaxMovementCancelThreshold)
            return;

        EndLongPress(endPosition, duration);
    }

    public void EvaluateTwoPointMoveUpdate (
        TouchPhase touch1Phase,
        TouchPhase touch2Phase,
        Vector2 touch1Position,
        Vector2 touch2Position
    )
    {
        bool touch1Active = touch1Phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary;
        bool touch2Active = touch2Phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary;

        if (!_twoPointMoveStarted)
        {
            if ((touch1Phase == TouchPhase.Began && touch2Active) ||
                (touch2Phase == TouchPhase.Began && touch1Active))
            {
                _twoPointMoveStarted = true;
                _twoPointMoveStartPosition = (touch1Position + touch2Position) * 0.5f;
                StartTwoPointMove(_twoPointMoveStartPosition);
            }
            
            return;
        }
        
        if (touch1Phase == TouchPhase.Ended || touch2Phase == TouchPhase.Ended)
        {
            _twoPointMoveStarted = false;
            return;
        }
        
        Vector2 middlePosition = (touch1Position + touch2Position) * 0.5f;
        Vector2 delta = middlePosition - _twoPointMoveStartPosition;
        PerformTwoPointMove(_twoPointMoveInputOptions.MoveSpeed * Time.deltaTime * delta / Screen.dpi);
        _twoPointMoveStartPosition = middlePosition;
    }

    public void EvaluateTwoPointZoomUpdate (
        Vector2 touch1Position,
        Vector2 touch2Position,
        Vector2 touch1DeltaPosition,
        Vector2 touch2DeltaPosition
    )
    {
        float previousDistance = (touch1Position - touch1DeltaPosition - (touch2Position - touch2DeltaPosition))
            .sqrMagnitude;
        float currentDistance = (touch1Position - touch2Position).sqrMagnitude;
        float difference = previousDistance - currentDistance;

        if (difference < _twoPointZoomInputOptions.MinZoomDistance)
            return;
        OnTwoPointZoomPerformed?.Invoke(difference);
    }

    public void EvaluateTouchDrag (TouchPhase touchPhase, Vector2 touchPosition)
    {
        switch (touchPhase)
        {
            case TouchPhase.Began:
                OnTouchDragBegan?.Invoke(touchPosition);
                break;
            case TouchPhase.Stationary:
            case TouchPhase.Moved:
                OnTouchDragMoved?.Invoke(touchPosition);
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                OnTouchDragEnded?.Invoke(touchPosition);
                break;
        }
    }
    
    void StartLongPress (Vector2 position)
    {
        _longPressStarted = true;
        OnLongPressStarted?.Invoke(position);
        DebugUtils.Log($"Long press started at: {position}");
    }
    
    void EndLongPress (Vector2 position, float duration)
    {
        _longPressStarted = false;
        OnLongPressEnded?.Invoke(position, duration);
        DebugUtils.Log($"Long press finished at: {position}");
    }

    void CancelLongPress (Vector2 position)
    {
        _longPressStarted = false;
        OnLongPressCancelled?.Invoke(position);
        DebugUtils.Log($"Long press cancelled at: {position}");
    }

    void StartTwoPointMove (Vector2 middlePosition)
    {
        OnTwoPointMoveStarted?.Invoke(middlePosition);
        DebugUtils.Log($"Two point started with middle: {middlePosition}");
    }

    void PerformTwoPointMove (Vector2 deltaPosition)
    {
        OnTwoPointMovePerformed?.Invoke(deltaPosition);
        DebugUtils.Log($"Two point moved by: {deltaPosition}");
    }
}