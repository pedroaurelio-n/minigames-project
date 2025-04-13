using System;
using UnityEngine;

public class PoolableView : MonoBehaviour
{
    public bool IsActive => gameObject.activeInHierarchy;
    
    Action _despawnCallback;

    public void Setup (Action despawnCallback) => _despawnCallback = despawnCallback;

    public void Despawn() => _despawnCallback();
}