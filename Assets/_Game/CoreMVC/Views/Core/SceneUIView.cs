using UnityEngine;

public class SceneUIView : MonoBehaviour
{
    [field: SerializeField] public FadeToBlackManager FadeToBlackManager { get; private set; }
    [field: SerializeField] public Transform ChangeSceneContainer { get; private set; }
}