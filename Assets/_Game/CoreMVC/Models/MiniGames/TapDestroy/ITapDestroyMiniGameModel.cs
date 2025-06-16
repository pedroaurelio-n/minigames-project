using System;
using UnityEngine;

public interface ITapDestroyMiniGameModel : IMiniGameModel
{
    event Action<IPressable, Vector2> OnTapPerformed;
    
    public int BaseObjectsToSpawn { get; }
}