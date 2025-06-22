using System;
using UnityEngine;

public interface ITapFloatingMiniGameModel : IMiniGameModel
{
    //TODO pedro: refactor this Pressable implementation
    event Action<IPressable, Vector2> OnTapPerformed;
    
    public int BaseTargetsToSpawn { get; }
    public int BaseObjectsToSpawn { get; }
}