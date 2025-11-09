using System;
using UnityEngine;

[Serializable]
public class DebugOptions
{
    [field: SerializeField] public bool EnableNormalLogs { get; private set; } = false;
    [field: SerializeField] public bool EnableWarnings { get; private set; } = false;
    [field: SerializeField] public bool EnableErrors { get; private set; } = false;
    [field: SerializeField] public bool DebugMode { get; private set; } = false;
}