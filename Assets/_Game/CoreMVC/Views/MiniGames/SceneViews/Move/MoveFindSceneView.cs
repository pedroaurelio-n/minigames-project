using UnityEngine;

public class MoveFindSceneView : SceneView
{
    [field: SerializeField] public FindableObjectView FindableObjectPrefab { get; private set; }
    [field: SerializeField] public Transform[] PossiblePoints { get; private set; }
    [field: SerializeField] public Transform[] AllPoints { get; private set; }
}