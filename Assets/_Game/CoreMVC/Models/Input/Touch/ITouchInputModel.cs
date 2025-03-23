using System;
using UnityEngine;

public delegate Action OnSwipePerformedHandler (
    Vector2 startPosition,
    Vector2 endPosition,
    Vector2 normalizedDirection,
    Vector2 rawDirection,
    float duration
);

public interface ITouchInputModel
{
    event Action<Vector2> OnTapPerformed;
    event OnSwipePerformedHandler OnSwipePerformed;
    event Action<Vector2> OnLongPressStarted;
    event Action<Vector2, float> OnLongPressEnded;
    event Action<Vector2> OnLongPressCancelled;
    event Action<Vector2> OnTwoPointMoveStarted;
    event Action<Vector2> OnTwoPointMovePerformed;
    event Action<float> OnTwoPointZoomPerformed;
    event Action<Vector2> OnTouchDragBegan;
    event Action<Vector2> OnTouchDragMoved;
    event Action<Vector2> OnTouchDragEnded;
    
    Camera MainCamera { get; }
    
    void SetMainCamera (Camera mainCamera);
    void PerformTap (Vector2 startPosition, Vector2 endPosition, float duration);
    void PerformSwipe (Vector2 startPosition, Vector2 endPosition, float duration);
    void EvaluateLongPressUpdate (
        Vector2 startPosition,
        Vector2 currentPosition,
        float startTime,
        ref bool longPressCancelled
    );
    void EvaluateLongPressEnd (
        Vector2 startPosition,
        Vector2 endPosition,
        float startTime,
        ref bool longPressCancelled
    );
    void EvaluateTwoPointMoveUpdate (
        TouchPhase touch1Phase,
        TouchPhase touch2Phase,
        Vector2 touch1Position,
        Vector2 touch2Position
    );
    void EvaluateTwoPointZoomUpdate (
        Vector2 touch1Position,
        Vector2 touch2Position,
        Vector2 touch1DeltaPosition,
        Vector2 touch2DeltaPosition
    );
    void EvaluateTouchDrag (TouchPhase touchPhase, Vector2 touchPosition);
}