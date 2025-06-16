using UnityEngine;

public class DragSortSceneView : SceneView
{
    [field: SerializeField] public DraggableObjectView DraggablePrefab { get; private set; }
    [field: SerializeField] public DraggableContainerView[] Containers { get; private set; }
    [field: SerializeField] public Transform BallSpawnPoint { get; private set; }
    [field: SerializeField] public Transform[] ContainerSpawnPoints { get; private set; }
}