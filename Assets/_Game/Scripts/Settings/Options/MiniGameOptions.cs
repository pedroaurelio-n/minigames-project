using System;
using UnityEngine;

[Serializable]
public class MiniGameOptions
{
    [field: SerializeField] public ButtonStopwatchMiniGameOptions ButtonStopwatchMiniGameOptions { get; private set; }
    [field: SerializeField] public DragSortMiniGameOptions DragSortMiniGameOptions { get; private set; }
    [field: SerializeField] public DragRemoveMiniGameOptions DragRemoveMiniGameOptions { get; private set; }
    [field: SerializeField] public JoystickRotateMiniGameOptions JoystickRotateMiniGameOptions { get; private set; }
    [field: SerializeField] public JoystickAimMiniGameOptions JoystickAimMiniGameOptions { get; private set; }
    [field: SerializeField] public LongPressBombsMiniGameOptions LongPressBombsMiniGameOptions { get; private set; }
    [field: SerializeField] public MoveFindMiniGameOptions MoveFindMiniGameOptions { get; private set; }
    [field: SerializeField] public SwipeThrowMiniGameOptions SwipeThrowMiniGameOptions { get; private set; }
    [field: SerializeField] public TapDestroyMiniGameOptions TapDestroyMiniGameOptions { get; private set; }
    [field: SerializeField] public TapFloatingMiniGameOptions TapFloatingMiniGameOptions { get; private set; }
    [field: SerializeField] public TapMovingMiniGameOptions TapMovingMiniGameOptions { get; private set; }
}

[Serializable]
public class ButtonStopwatchMiniGameOptions
{
    [field: SerializeField] public float SuccessThreshold { get; private set; }
}

[Serializable]
public class DragSortMiniGameOptions
{
    [field: SerializeField] public float BallSpawnDistance { get; private set; }
    [field: SerializeField] public float ContainerHorizontalDistance { get; private set; }
}

[Serializable]
public class DragRemoveMiniGameOptions
{
    [field: SerializeField] public Vector2 SpawnRange { get; private set; }
}

[Serializable]
public class JoystickRotateMiniGameOptions
{
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float WinAngleTolerance { get; private set; }
}

[Serializable]
public class JoystickAimMiniGameOptions
{
    [field: SerializeField] public float RotationSpeed { get; private set; }
    [field: SerializeField] public float ShootDelay { get; private set; }
    [field: SerializeField] public float ShootSpeed { get; private set; }
    [field: SerializeField] public Vector2 SpawnRange { get; private set; }
}

[Serializable]
public class LongPressBombsMiniGameOptions
{
    [field: SerializeField] public Vector2 SpawnRange { get; private set; }
    [field: SerializeField] public Vector2 DelayRange { get; private set; }
    [field: SerializeField] public Vector2 TimerRange { get; private set; }
    [field: SerializeField] public Vector2 DefuseRange { get; private set; }
    [field: SerializeField] public float TimerGrace { get; private set; }
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
public class SwipeThrowMiniGameOptions
{
    [field: SerializeField] public float DirectionWeight { get; private set; }
    [field: SerializeField] public float ForwardWeight { get; private set; }
    [field: SerializeField] public float ThrowDelay { get; private set; }
}

[Serializable]
public class TapDestroyMiniGameOptions
{
    [field: SerializeField] public Vector2 SpawnRange { get; private set; }
}

[Serializable]
public class TapFloatingMiniGameOptions
{
    [field: SerializeField] public Vector2 SpeedRange { get; private set; }
    [field: SerializeField] public Vector2 DelayRange { get; private set; }
    [field: SerializeField] public float XSpawnDistance { get; private set; }
    [field: SerializeField] public Vector2 YDistanceRange { get; private set; }
}

[Serializable]
public class TapMovingMiniGameOptions
{
    [field: SerializeField] public Vector2 SpawnRange { get; private set; }
    [field: SerializeField] public Vector2 SpeedRange { get; private set; }
    [field: SerializeField] public Vector2 DelayRange { get; private set; }
}