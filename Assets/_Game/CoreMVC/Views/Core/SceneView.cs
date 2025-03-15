using UnityEngine;

public class SceneView : MonoBehaviour
{
    public MouseInputView MouseInput { get; private set; }

    public void Initialize ()
    {
        CreateInputs();
    }

    void CreateInputs ()
    {
        GameObject mouseInput = new("MouseInput");
        mouseInput.transform.SetParent(transform);
        MouseInput = mouseInput.AddComponent<MouseInputView>();
    }
}