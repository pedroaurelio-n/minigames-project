using System;
using UnityEngine;

public class TouchInputController : IDisposable
{
    readonly ITouchInputModel _touchInputModel;
    readonly TapInputView _tapInputView;
    readonly SwipeInputView _swipeInputView;
    readonly LongPressInputView _longPressInputView;
    readonly TwoPointMoveInputView _twoPointMoveInputView;
    readonly TwoPointZoomInputView _twoPointZoomInputView;
    readonly TouchDragInputView _touchDragInputView;
    
    float _tapStartTime;
    Vector2 _tapStartPosition;

    float _swipeStartTime;
    Vector2 _swipeStartPosition;
    
    float _longPressStartTime;
    Vector2 _longPressStartPosition;
    bool _longPressCancelled;
    
    public TouchInputController (
        ITouchInputModel touchInputModel,
        TapInputView tapInputView,
        SwipeInputView swipeInputView,
        LongPressInputView longPressInputView,
        TwoPointMoveInputView twoPointMoveInputView,
        TwoPointZoomInputView twoPointZoomInputView,
        TouchDragInputView touchDragInputView
    )
    {
        _touchInputModel = touchInputModel;
        _tapInputView = tapInputView;
        _swipeInputView = swipeInputView;
        _longPressInputView = longPressInputView;
        _twoPointMoveInputView = twoPointMoveInputView;
        _twoPointZoomInputView = twoPointZoomInputView;
        _touchDragInputView = touchDragInputView;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        _tapInputView.OnTapBegan += HandleTapBegan;
        _tapInputView.OnTapEnded += HandleTapEnded;

        _swipeInputView.OnSwipeBegan += HandleSwipeBegan;
        _swipeInputView.OnSwipeEnded += HandleSwipeEnded;
        
        _longPressInputView.OnLongPressBegan += HandleLongPressBegan;
        _longPressInputView.OnLongPressUpdated += HandleLongPressUpdated;
        _longPressInputView.OnLongPressEnded += HandleLongPressEnded;

        _twoPointMoveInputView.OnTwoPointMoveUpdated += HandleTwoPointMoveUpdated;
        
        _twoPointZoomInputView.OnTwoPointZoomUpdated += HandleTwoPointZoomUpdated;

        _touchDragInputView.OnTouchDragUpdated += HandleTouchDragUpdated;
    }

    void RemoveListeners ()
    {
        _tapInputView.OnTapBegan -= HandleTapBegan;
        _tapInputView.OnTapEnded -= HandleTapEnded;

        _swipeInputView.OnSwipeBegan -= HandleSwipeBegan;
        _swipeInputView.OnSwipeEnded -= HandleSwipeEnded;
        
        _longPressInputView.OnLongPressBegan -= HandleLongPressBegan;
        _longPressInputView.OnLongPressUpdated -= HandleLongPressUpdated;
        _longPressInputView.OnLongPressEnded -= HandleLongPressEnded;

        _twoPointMoveInputView.OnTwoPointMoveUpdated -= HandleTwoPointMoveUpdated;
        
        _twoPointZoomInputView.OnTwoPointZoomUpdated -= HandleTwoPointZoomUpdated;

        _touchDragInputView.OnTouchDragUpdated -= HandleTouchDragUpdated;
    }

    void HandleTapBegan (Touch touch)
    {
        _tapStartTime = Time.time;
        _tapStartPosition = touch.position;
    }

    void HandleTapEnded (Touch touch)
    {
        float duration = Time.time - _tapStartTime;
        _touchInputModel.PerformTap(_tapStartPosition, touch.position, duration);
    }

    void HandleSwipeBegan (Touch touch)
    {
        _swipeStartTime = Time.time;
        _swipeStartPosition = touch.position;
    }

    void HandleSwipeEnded (Touch touch)
    {
        float duration = Time.time - _swipeStartTime;
        _touchInputModel.PerformSwipe(_swipeStartPosition, touch.position, duration);
    }
    
    void HandleLongPressBegan (Touch touch)
    {
        _longPressStartTime = Time.time;
        _longPressStartPosition = touch.position;
        _longPressCancelled = false;
    }
    
    void HandleLongPressUpdated (Touch touch)
    {
        _touchInputModel.EvaluateLongPressUpdate(
            _longPressStartPosition,
            touch.position,
            _longPressStartTime,
            ref _longPressCancelled
        );
    }

    void HandleLongPressEnded (Touch touch)
    {
        _touchInputModel.EvaluateLongPressEnd(
            _longPressStartPosition,
            touch.position,
            _longPressStartTime,
            ref _longPressCancelled
        );
    }

    void HandleTwoPointMoveUpdated (Touch touch1, Touch touch2)
    {
        _touchInputModel.EvaluateTwoPointMoveUpdate(touch1.phase, touch2.phase, touch1.position, touch2.position);
    }
    
    void HandleTwoPointZoomUpdated (Touch touch1, Touch touch2)
    {
        _touchInputModel.EvaluateTwoPointZoomUpdate(
            touch1.position,
            touch2.position,
            touch1.deltaPosition,
            touch2.deltaPosition
        );
    }

    void HandleTouchDragUpdated (Touch touch)
    {
        _touchInputModel.EvaluateTouchDrag(touch.phase, touch.position);
    }

    public void Dispose ()
    {
        RemoveListeners();
    }
}