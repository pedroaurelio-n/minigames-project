using System;
using UnityEngine;

public interface ITapMovingMinigameModel : IMiniGameModel
{
    event Action<ITappable, Vector2> OnTapPerformed;
    
    public int BaseObjectsToSpawn { get; }
}