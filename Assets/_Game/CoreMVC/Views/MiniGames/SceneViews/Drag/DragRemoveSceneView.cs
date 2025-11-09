using UnityEngine;

public class DragRemoveSceneView : SceneView
{
    [field: SerializeField] public DraggableClutterObjectView[] ClutterPrefabs { get; private set; }
    [field: SerializeField] public DraggableObjectiveView ObjectivePrefab { get; private set; }
}