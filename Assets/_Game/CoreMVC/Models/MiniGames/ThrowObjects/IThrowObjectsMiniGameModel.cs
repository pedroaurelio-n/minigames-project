using System;
using UnityEngine;

public interface IThrowObjectsMiniGameModel : IMiniGameModel
{
    event Action<Vector3> OnSwipePerformed;
}