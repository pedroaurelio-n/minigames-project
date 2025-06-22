using UnityEngine;

public class TapFloatingSceneView : SceneView
{
    [field: SerializeField] public FloatingObjectView FloatingObjectView { get; private set; }
}