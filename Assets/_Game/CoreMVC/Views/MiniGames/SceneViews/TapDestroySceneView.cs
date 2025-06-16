using UnityEngine;

public class TapDestroySceneView : SceneView
{
    [field: SerializeField] public TappableObjectView TappableObjectPrefab { get; private set; }
}