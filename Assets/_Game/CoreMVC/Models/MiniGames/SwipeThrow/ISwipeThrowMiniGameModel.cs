using System;
using UnityEngine;

public interface ISwipeThrowMiniGameModel : IMiniGameModel
{
    event Action<Vector3, Vector3> OnSwipePerformed;
}