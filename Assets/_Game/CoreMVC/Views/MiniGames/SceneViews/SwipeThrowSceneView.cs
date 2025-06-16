using UnityEngine;

public class SwipeThrowSceneView : SceneView
{
    [field: SerializeField] public ThrowableObjectView ThrowableObjectPrefab { get; private set; }
    [field: SerializeField] public ThrowableContainerView Container { get; private set; }
    [field: SerializeField] public Transform ThrowableSpawnPoint { get; private set; }
    [field: SerializeField] public Transform[] ContainerSpawnPoints { get; private set; }
}