using System;
using UnityEngine;

[Serializable]
public class MiniGameOptions
{
    [field: SerializeField] public TapObjectsMiniGameOptions TapObjectsMiniGameOptions { get; private set; }
    [field: SerializeField] public DragObjectsMiniGameOptions DragObjectsMiniGameOptions { get; private set; }
    [field: SerializeField] public ThrowObjectsMiniGameOptions ThrowObjectsMiniGameOptions { get; private set; }
    [field: SerializeField] public FindObjectMiniGameOptions FindObjectMiniGameOptions { get; private set; }
}

[Serializable]
public class TapObjectsMiniGameOptions
{
    [field: SerializeField] public Vector2 SpawnDistance { get; private set; }
}

[Serializable]
public class DragObjectsMiniGameOptions
{
    [field: SerializeField] public float BallSpawnDistance { get; private set; }
    [field: SerializeField] public float ContainerHorizontalDistance { get; private set; }
}

[Serializable]
public class ThrowObjectsMiniGameOptions
{
    [field: SerializeField] public float DirectionWeight { get; private set; }
    [field: SerializeField] public float ForwardWeight { get; private set; }
    [field: SerializeField] public float ThrowDelay { get; private set; }
}

[Serializable]
public class FindObjectMiniGameOptions
{
    [field: SerializeField] public float CheckDelay { get; private set; }
    [field: SerializeField] public Vector2 DistanceRange { get; private set; }
}