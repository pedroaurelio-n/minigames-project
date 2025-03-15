using UnityEngine;

public class TouchInputController
{
    readonly ITouchInputModel _touchInputModel;
    readonly TapInputView _tapInputView;

    TapInputOptions _tapInputOptions;
    float startTapTime;
    Vector2 startTapPosition;
    
    public TouchInputController (
        ITouchInputModel touchInputModel,
        TapInputView tapInputView
    )
    {
        _touchInputModel = touchInputModel;
        _tapInputView = tapInputView;

        _tapInputOptions = GameGlobalOptions.Instance.TapInputOptions;
    }

    public void Initialize ()
    {
        AddListeners();
    }

    void AddListeners ()
    {
        _tapInputView.OnTapBegan += HandleTapBegan;
        _tapInputView.OnTapEnded += HandleTapEnded;
    }

    void RemoveListeners ()
    {
        //TODO pedro: 
    }

    void HandleTapBegan (Touch touch)
    {
        startTapTime = Time.time;
        startTapPosition = touch.position;
    }

    void HandleTapEnded (Touch touch)
    {
        float touchDuration = Time.time - startTapTime;
        float touchDistanceSquared = (touch.position - startTapPosition).sqrMagnitude;

        if (touchDuration <= _tapInputOptions.TapTimeThreshold &&
            touchDistanceSquared <= _tapInputOptions.TapMovementThreshold * _tapInputOptions.TapMovementThreshold)
        {
            _touchInputModel.PerformTap(touch.position);
        }
    }
}