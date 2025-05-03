using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(fileName = "GameGlobalOptions", menuName = "GameGlobalOptions")]
public class GameGlobalOptions : ScriptableObject
{
    [field: SerializeField] public DebugOptions DebugOptions { get; private set; }
    [field: SerializeField] public LayerMaskOptions LayerMaskOptions { get; private set; }
    [field: SerializeField] public FadeTransitionOptions FadeTransitionOptions { get; private set; }
    [field: SerializeField] public InputOptions InputOptions { get; private set; }
    
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
