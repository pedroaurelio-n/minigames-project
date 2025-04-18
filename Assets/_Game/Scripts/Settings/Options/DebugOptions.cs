using System;
using UnityEngine;

[Serializable]
public class DebugOptions
{
    [field: SerializeField] public bool EnableLogging { get; private set; } = false;
    [field: SerializeField] public bool DebugMode { get; private set; } = false;
}