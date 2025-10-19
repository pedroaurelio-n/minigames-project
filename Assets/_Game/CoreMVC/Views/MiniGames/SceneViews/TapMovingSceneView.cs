using UnityEngine;

public class TapMovingSceneView : SceneView
{
    [field: SerializeField] public TappableMovingObjectView TappableMovingObjectPrefab { get; private set; }
}