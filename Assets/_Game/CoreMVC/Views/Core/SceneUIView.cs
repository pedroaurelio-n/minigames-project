using UnityEngine;

public class SceneUIView : MonoBehaviour
{
    [field: SerializeField] public Transform ChangeSceneContainer { get; private set; }
    [field: SerializeField] public MiniGameLabelUIView MiniGameLabelUIView { get; private set; }
    [field: SerializeField] public MiniGameTimerUIView MiniGameTimerUIView { get; private set; }
    [field: SerializeField] public Transform PriorityHUD { get; private set; }
}