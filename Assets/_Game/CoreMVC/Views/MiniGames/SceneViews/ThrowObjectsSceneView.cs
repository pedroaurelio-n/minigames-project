using UnityEngine;

public class ThrowObjectsSceneView : SceneView
{
    [field: SerializeField] public ThrowableObjectView ThrowableObjectPrefab { get; private set; }
    [field: SerializeField] public ThrowableContainerView Container { get; private set; }
    [field: SerializeField] public Transform SpawnPoint { get; private set; }
}