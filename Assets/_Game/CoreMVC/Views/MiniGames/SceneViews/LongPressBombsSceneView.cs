using UnityEngine;

public class LongPressBombsSceneView : SceneView
{
    [field: SerializeField] public LongPressableBombView BombPrefab { get; private set; }
}