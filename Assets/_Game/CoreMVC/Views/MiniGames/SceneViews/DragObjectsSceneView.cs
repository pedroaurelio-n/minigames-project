using UnityEngine;

public class DragObjectsSceneView : SceneView
{
    [field: SerializeField] public DraggableObjectView DraggablePrefab { get; private set; }
    [field: SerializeField] public DraggableContainerView[] Containers { get; private set; }
    [field: SerializeField] public Transform SpawnPoint { get; private set; }
}