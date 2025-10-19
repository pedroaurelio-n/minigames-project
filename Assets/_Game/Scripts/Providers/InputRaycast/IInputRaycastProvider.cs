using UnityEngine;

public interface IInputRaycastProvider
{
    bool TryGetRaycastHit (Vector2 touchPosition, LayerMask layerMask, out RaycastHit hit);
    bool TryGetHitComponent<T> (Vector2 touchPosition, LayerMask layerMask, out T component) where T : class;
}