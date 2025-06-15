using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class SceneView : MonoBehaviour, IDisposable
{
    public MouseInputView MouseInput { get; private set; }
    public TapInputView TapInputView { get; private set; }
    public SwipeInputView SwipeInputView { get; private set; }
    public LongPressInputView LongPressInputView { get; private set; }
    public TwoPointMoveInputView TwoPointMoveInputView { get; private set; }
    public TwoPointZoomInputView TwoPointZoomInputView { get; private set; }
    public TouchDragInputView TouchDragInputView { get; private set; }

    public void Initialize ()
    {
        EnhancedTouchSupport.Enable();
        CreateInputs();
    }

    public void SetActiveInputs (TouchInputType activeInputs)
    {
        TapInputView.enabled = (activeInputs & TouchInputType.Tap) != 0;
        SwipeInputView.enabled = (activeInputs & TouchInputType.Swipe) != 0;
        LongPressInputView.enabled = (activeInputs & TouchInputType.LongPress) != 0;
        TwoPointMoveInputView.enabled = (activeInputs & TouchInputType.TwoPointMove) != 0;
        TwoPointZoomInputView.enabled = (activeInputs & TouchInputType.TwoPointZoom) != 0;
        TouchDragInputView.enabled = (activeInputs & TouchInputType.Drag) != 0;
    }

    void CreateInputs ()
    {
        //TODO pedro: delete mouse input classes
        // GameObject mouseInput = new("MouseInput");
        // mouseInput.transform.SetParent(transform);
        // MouseInput = mouseInput.AddComponent<MouseInputView>();

        GameObject touchInput = new("TouchInputView");
        touchInput.transform.SetParent(transform);
        TapInputView = touchInput.AddComponent<TapInputView>();
        SwipeInputView = touchInput.AddComponent<SwipeInputView>();
        LongPressInputView = touchInput.AddComponent<LongPressInputView>();
        TwoPointMoveInputView = touchInput.AddComponent<TwoPointMoveInputView>();
        TwoPointZoomInputView = touchInput.AddComponent<TwoPointZoomInputView>();
        TouchDragInputView = touchInput.AddComponent<TouchDragInputView>();
    }

    public void Dispose ()
    {
        EnhancedTouchSupport.Disable();
    }
}