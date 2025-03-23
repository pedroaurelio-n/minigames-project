using System;
using UnityEngine;

public interface IPressModel : IDisposable
{
    event Action<IPressable, Vector2> OnTapPerformed;
    
    void Initialize ();
}