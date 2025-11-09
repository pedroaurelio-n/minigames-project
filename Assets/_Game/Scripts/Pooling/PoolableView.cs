using System;
using UnityEngine;

public class PoolableView : MonoBehaviour
{
    public bool IsActive => gameObject.activeInHierarchy;
    public int PoolIndex { get; private set; }
    
    Action _despawnCallback;

    public void SetupView (Action despawnCallback, int poolIndex)
    {
        _despawnCallback = despawnCallback;
        PoolIndex = poolIndex;
    }

    public void Despawn() => _despawnCallback();
}