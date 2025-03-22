using System;
using UnityEngine;

[Serializable]
public class InputOptions
{
    [field: SerializeField] public TapInputOptions TapInputOptions { get; private set; }
    [field: SerializeField] public SwipeInputOptions SwipeInputOptions { get; private set; }
    [field: SerializeField] public LongPressInputOptions LongPressInputOptions { get; private set; }
    [field: SerializeField] public TwoPointMoveInputOptions TwoPointMoveInputOptions { get; private set; }
    [field: SerializeField] public TwoPointZoomInputOptions TwoPointZoomInputOptions { get; private set; }
}

[Serializable]
public class TapInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MaxTimeThreshold { get; private set; } = 0.3f;
    [field: SerializeField] public float MaxMovementThreshold { get; private set; } = 15f;
}

[Serializable]
public class SwipeInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MaxTimeThreshold { get; private set; } = 1.25f;
    [field: SerializeField] public float MinMovementThreshold { get; private set; } = 100f;
    [field: SerializeField] public float DiagonalThreshold { get; private set; } = 0.5f;
}

[Serializable]
public class LongPressInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MinTimeThreshold { get; private set; } = 1f;
    [field: SerializeField] public float MaxMovementStartThreshold { get; private set; } = 25f;
    [field: SerializeField] public float MaxMovementCancelThreshold { get; private set; } = 100f;
}

[Serializable]
public class TwoPointMoveInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;
}

[Serializable]
public class TwoPointZoomInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MinZoomDistance { get; private set; } = 10f;
}