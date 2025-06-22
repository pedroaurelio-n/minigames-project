using System.Collections.Generic;
using UnityEngine;

public class PoolableViewFactory
{
    public Transform Container { get; }
    
    readonly Dictionary<string, ObjectPool<PoolableView>> _pools = new();

    public PoolableViewFactory(Transform container)
    {
        Container = container;
    }
    
    public void SetupPool<T> (T prefab) where T : PoolableView
    {
        SetupPool<T>(typeof(T).Name, prefab);
    }

    public void SetupPool<T> (string poolName, T prefab) where T : PoolableView
    {
        _pools.TryAdd(poolName, new ObjectPool<PoolableView>(prefab, this));
    }
    
    public T GetView<T> (Transform container) where T : PoolableView
    {
        return GetView<T>(typeof(T).Name, container);
    }

    public T GetView<T> (string poolName, Transform container) where T : PoolableView
    {
        if (!_pools.TryGetValue(poolName, out ObjectPool<PoolableView> pool))
        {
            DebugUtils.LogError(
                $"Trying to get a null poolable object. Make sure the pool was setup for {typeof(T).Name}",
                true
            );
            return default;
        }

        T poolableObj = pool.Get() as T;
        poolableObj.transform.SetParent(container);
        
        return poolableObj;
    }
    
    public void ReleaseView (PoolableView view)
    {
        ReleaseView(view.GetType().Name, view);
    }
    
    public void ReleaseView (string poolName, PoolableView view)
    {
        if (!_pools.TryGetValue(poolName, out ObjectPool<PoolableView> pool))
            return;
        
        pool.Release(view);
    }
}