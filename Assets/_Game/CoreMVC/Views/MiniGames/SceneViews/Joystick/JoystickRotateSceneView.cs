using UnityEngine;

public class JoystickRotateSceneView : SceneView
{
    [field: SerializeField] public Transform RotatingObject { get; private set; }
    [field: SerializeField] public Transform Target { get; private set; }
}
