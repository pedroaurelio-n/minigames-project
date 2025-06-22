using System;
using UnityEngine;

[Serializable]
public class MiniGameOptions
{
    [field: SerializeField] public TapDestroyMiniGameOptions TapDestroyMiniGameOptions { get; private set; }
    [field: SerializeField] public DragSortMiniGameOptions DragSortMiniGameOptions { get; private set; }
    [field: SerializeField] public SwipeThrowMiniGameOptions SwipeThrowMiniGameOptions { get; private set; }
    [field: SerializeField] public MoveFindMiniGameOptions MoveFindMiniGameOptions { get; private set; }
    [field: SerializeField] public TapFloatingMiniGameOptions TapFloatingMiniGameOptions { get; private set; }
}

[Serializable]
public class TapDestroyMiniGameOptions
{
    [field: SerializeField] public Vector2 SpawnDistance { get; private set; }
}

[Serializable]
public class DragSortMiniGameOptions
{
    [field: SerializeField] public float BallSpawnDistance { get; private set; }
    [field: SerializeField] public float ContainerHorizontalDistance { get; private set; }
}

[Serializable]
public class SwipeThrowMiniGameOptions
{
    [field: SerializeField] public float DirectionWeight { get; private set; }
    [field: SerializeField] public float ForwardWeight { get; private set; }
    [field: SerializeField] public float ThrowDelay { get; private set; }
}

[Serializable]
public class MoveFindMiniGameOptions
{
    [field: SerializeField] public float CheckDelay { get; private set; }
    [field: SerializeField] public Vector2 DistanceRange { get; private set; }
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float ZoomSpeed { get; private set; }
    [field: SerializeField] public float MinCameraZoom { get; private set; }
    [field: SerializeField] public float MaxCameraZoom { get; private set; }
}

[Serializable]
public class TapFloatingMiniGameOptions
{
    [field: SerializeField] public Vector2 SpeedRange { get; private set; }
    [field: SerializeField] public Vector2 DelayRange { get; private set; }
    [field: SerializeField] public float XSpawnDistance { get; private set; }
    [field: SerializeField] public Vector2 YDistanceRange { get; private set; }
}