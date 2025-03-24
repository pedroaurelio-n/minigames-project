using UnityEngine;

public class TapObjectsSceneView : SceneView
{
    [field: SerializeField] public TappableObjectView TappableObjectPrefab { get; private set; }
}