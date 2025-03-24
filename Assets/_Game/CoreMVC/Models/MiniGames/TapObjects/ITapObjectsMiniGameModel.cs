using System;
using UnityEngine;

public interface ITapObjectsMiniGameModel : IMiniGameModel
{
    event Action<IPressable, Vector2> OnTapPerformed;
    
    public int BaseObjectsToSpawn { get; }
    public int MaxSpawnDistance { get; }
}