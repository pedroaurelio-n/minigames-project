using System;
using UnityEngine;

public interface IPressModel : IDisposable
{
    event Action<ITappable, Vector2> OnTapPerformed;
    event Action<ILongPressable, Vector2> OnLongPressBegan;
    event Action<ILongPressable, Vector2> OnLongPressCancelled;
    event Action<ILongPressable, Vector2, float> OnLongPressEnded;
    
    void Initialize ();
}