using System;
using UnityEngine;

public interface ILongPressBombsMiniGameModel : IMiniGameModel
{
    event Action<ILongPressable, Vector2> OnLongPressBegan;
    event Action<ILongPressable, Vector2> OnLongPressCancelled;
    event Action<ILongPressable, Vector2, float> OnLongPressEnded;
    
    public int BaseObjectsToSpawn { get; }
}