using System;
using UnityEngine;

[Serializable]
public class MiniGameOptions
{
    [field: SerializeField] public float BaseMiniGameDuration { get; private set; } = 10f;
}