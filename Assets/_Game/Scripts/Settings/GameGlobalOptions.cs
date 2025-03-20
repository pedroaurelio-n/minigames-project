using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "GameGlobalOptions", menuName = "GameGlobalOptions")]
public class GameGlobalOptions : ScriptableObject
{
    [field: SerializeField] public LayerMaskOptions LayerMaskOptions { get; private set; }
    [field: SerializeField] public FadeTransitionOptions FadeTransitionOptions { get; private set; }
    [field: SerializeField] public TapInputOptions TapInputOptions { get; private set; }
    [field: SerializeField] public SwipeInputOptions SwipeInputOptions { get; private set; }
    [field: SerializeField] public LongPressInputOptions LongPressInputOptions { get; private set; }
    [field: SerializeField] public TwoPointMoveInputOptions TwoPointMoveInputOptions { get; private set; }

    public static GameGlobalOptions Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            
            _instance = Resources.Load<GameGlobalOptions>("GameGlobalOptions");
            if (_instance == null)
                throw new NullReferenceException($"GameGlobalOptions instance was not found in Resources.");

            return _instance;
        }
    }

    static GameGlobalOptions _instance;
}

[Serializable]
public class LayerMaskOptions
{
    [field: SerializeField] public LayerMask InteractableLayers { get; private set; }
}

[Serializable]
public class FadeTransitionOptions
{
    [field: SerializeField] public float Duration { get; private set; } = 0.8f;
}

[Serializable]
public class TapInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MaxTimeThreshold { get; private set; }
    [field: SerializeField] public float MaxMovementThreshold { get; private set; }
}

[Serializable]
public class SwipeInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MaxTimeThreshold { get; private set; }
    [field: SerializeField] public float MinMovementThreshold { get; private set; }
    [field: SerializeField] public float DiagonalThreshold { get; private set; }
}

[Serializable]
public class LongPressInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MinTimeThreshold { get; private set; }
    [field: SerializeField] public float MaxMovementStartThreshold { get; private set; }
    [field: SerializeField] public float MaxMovementCancelThreshold { get; private set; }
}

[Serializable]
public class TwoPointMoveInputOptions : BaseInputOptions
{
    [field: SerializeField] public float MoveSpeed { get; private set; }
}

[Serializable]
public abstract class BaseInputOptions
{
    public void SetValues(Dictionary<string, object> values)
    {
        PropertyInfo[] fields = GetType().GetProperties(
            BindingFlags.Public
            | BindingFlags.NonPublic
            | BindingFlags.Instance
        );

        foreach (var field in fields)
        {
            if (values.TryGetValue(field.Name, out object value))
                field.SetValue(this, value);
        }
    }
}
