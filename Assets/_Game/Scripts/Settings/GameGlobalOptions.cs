using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameGlobalOptions", menuName = "GameGlobalOptions")]
public class GameGlobalOptions : ScriptableObject
{
    [field: SerializeField] public LayerMasksOptions LayerMasks { get; private set; }
    [field: SerializeField] public FadeTransitionOptions FadeTransition { get; private set; }
    [field: SerializeField] public TapInputOptions TapInputOptions { get; private set; }

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
public class LayerMasksOptions
{
    [field: SerializeField] public LayerMask InteractableLayers { get; private set; }
}

[Serializable]
public class FadeTransitionOptions
{
    [field: SerializeField] public float FadeDuration { get; private set; } = 0.8f;
}

[Serializable]
public class TapInputOptions
{
    [field: SerializeField] public float TapTimeThreshold { get; private set; }
    [field: SerializeField] public float TapMovementThreshold { get; private set; }
}
