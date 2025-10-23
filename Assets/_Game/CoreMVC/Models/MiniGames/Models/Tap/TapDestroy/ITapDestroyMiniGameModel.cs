using System;
using UnityEngine;

public interface ITapDestroyMiniGameModel : IMiniGameModel
{
    event Action<ITappable, Vector2> OnTapPerformed;
    
    public int BaseObjectsToSpawn { get; }
}