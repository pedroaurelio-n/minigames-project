using UnityEngine;

public class TouchInputController
{
    readonly ITouchInputModel _touchInputModel;
    readonly TapInputView _tapInputView;
    readonly SwipeInputView _swipeInputView;
    readonly LongPressInputView _longPressInputView;
    readonly TwoPointMoveInputView _twoPointMoveInputView;
    
    readonly TapInputOptions _tapInputOptions;
    readonly SwipeInputOptions _swipeInputOptions;
    readonly LongPressInputOptions _longPressInputOptions;
    readonly TwoPointMoveInputOptions _twoPointMoveInputOptions;
    
    float _tapStartTime;
    Vector2 _tapStartPosition;

    float _swipeStartTime;
    Vector2 _swipeStartPosition;
    
    float _longPressStartTime;
    Vector2 _longPressStartPosition;
    bool _longPressCancelled;

    bool _twoPointMoveStarted;
    Vector2 _twoPointMoveStartPosition;
    
    public TouchInputController (
        ITouchInputModel touchInputModel,
        TapInputView tapInputView,
        SwipeInputView swipeInputView,
        LongPressInputView longPressInputView,
        TwoPointMoveInputView twoPointMoveInputView
    )
    {
        _touchInputModel = touchInputModel;
        _tapInputView = tapInputView;
        _swipeInputView = swipeInputView;
        _longPressInputView = longPressInputView;
        _twoPointMoveInputView = twoPointMoveInputView;

        _tapInputOptions = GameGlobalOptions.Instance.TapInputOptions;
        _swipeInputOptions = GameGlobalOptions.Instance.SwipeInputOptions;
        _longPressInputOptions = GameGlobalOptions.Instance.LongPressInputOptions;
        _twoPointMoveInputOptions = GameGlobalOptions.Instance.TwoPointMoveInputOptions;
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
    }

    void RemoveListeners ()
    {
        //TODO pedro: 
    }

    void HandleTapBegan (Touch touch)
    {
        _tapStartTime = Time.time;
        _tapStartPosition = touch.position;
    }

    void HandleTapEnded (Touch touch)
    {
        float duration = Time.time - _tapStartTime;
        float distanceSquared = (touch.position - _tapStartPosition).sqrMagnitude;

        if (duration > _tapInputOptions.MaxTimeThreshold
            || distanceSquared > _tapInputOptions.MaxMovementThreshold * _tapInputOptions.MaxMovementThreshold)
            return;

        _touchInputModel.PerformTap(touch.position);
    }

    void HandleSwipeBegan (Touch touch)
    {
        _swipeStartTime = Time.time;
        _swipeStartPosition = touch.position;
    }

    void HandleSwipeEnded (Touch touch)
    {
        float duration = Time.time - _swipeStartTime;
        Vector2 endPosition = touch.position;
        Vector2 delta = endPosition - _swipeStartPosition;

        if (duration > _swipeInputOptions.MaxTimeThreshold
            || delta.sqrMagnitude < _swipeInputOptions.MinMovementThreshold * _swipeInputOptions.MinMovementThreshold)
            return;
        
        _touchInputModel.PerformSwipe(delta, _swipeInputOptions.DiagonalThreshold);
    }
    
    void HandleLongPressBegan (Touch touch)
    {
        _longPressStartTime = Time.time;
        _longPressStartPosition = touch.position;
        _longPressCancelled = false;
    }
    
    void HandleLongPressUpdated (Touch touch)
    {
        float distanceSquared = (touch.position - _longPressStartPosition).sqrMagnitude;

        if (_touchInputModel.LongPressStarted)
        {
            if (distanceSquared > _longPressInputOptions.MaxMovementCancelThreshold
                * _longPressInputOptions.MaxMovementCancelThreshold)
            {
                _touchInputModel.CancelLongPress(touch.position);
                _longPressCancelled = true;
            }
            return;
        }
        
        float duration = Time.time - _longPressStartTime;
        if (duration < _longPressInputOptions.MinTimeThreshold
            || distanceSquared > _longPressInputOptions.MaxMovementStartThreshold
            * _longPressInputOptions.MaxMovementStartThreshold)
            return;
        
        _touchInputModel.StartLongPress(touch.position);
    }

    void HandleLongPressEnded (Touch touch)
    {
        if (_longPressCancelled)
            return;
        
        float duration = Time.time - _longPressStartTime;
        if (duration < _longPressInputOptions.MinTimeThreshold)
            return;
        
        if (!_touchInputModel.LongPressStarted)
        {
            _touchInputModel.CancelLongPress(touch.position);
            return;
        }

        float distanceSquared = (touch.position - _longPressStartPosition).sqrMagnitude;
        if (distanceSquared > _longPressInputOptions.MaxMovementCancelThreshold
            * _longPressInputOptions.MaxMovementCancelThreshold)
            return;

        _touchInputModel.EndLongPress(touch.position);
    }

    void HandleTwoPointMoveUpdated (Touch touch1, Touch touch2)
    {
        bool touch1Active = touch1.phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary;
        bool touch2Active = touch2.phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary;

        if (!_twoPointMoveStarted)
        {
            if ((touch1.phase == TouchPhase.Began && touch2Active) ||
                (touch2.phase == TouchPhase.Began && touch1Active))
            {
                _twoPointMoveStarted = true;
                _twoPointMoveStartPosition = (touch1.position + touch2.position) * 0.5f;
                _touchInputModel.StartTwoPointMove(_twoPointMoveStartPosition);
            }
            
            return;
        }
        
        if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
        {
            _twoPointMoveStarted = false;
            return;
        }
        
        Vector2 middlePosition = (touch1.position + touch2.position) * 0.5f;
        Vector2 delta = middlePosition - _twoPointMoveStartPosition;
        _touchInputModel.PerformTwoPointMove(_twoPointMoveInputOptions.MoveSpeed * Time.deltaTime * delta / Screen.dpi);
        _twoPointMoveStartPosition = middlePosition;
    }
}