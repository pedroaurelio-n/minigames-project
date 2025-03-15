using UnityEngine;

public class SceneView : MonoBehaviour
{
    public MouseInputView MouseInput { get; private set; }
    public TapInputView TapInputView { get; private set; }
    public SwipeInputView SwipeInputView { get; private set; }
    public LongPressInputView LongPressInputView { get; private set; }

    public void Initialize ()
    {
        CreateInputs();
    }

    void CreateInputs ()
    {
        GameObject mouseInput = new("MouseInput");
        mouseInput.transform.SetParent(transform);
        MouseInput = mouseInput.AddComponent<MouseInputView>();

        GameObject touchInput = new("TouchInputView");
        touchInput.transform.SetParent(transform);
        TapInputView = touchInput.AddComponent<TapInputView>();
        SwipeInputView = touchInput.AddComponent<SwipeInputView>();
        LongPressInputView = touchInput.AddComponent<LongPressInputView>();
    }
}