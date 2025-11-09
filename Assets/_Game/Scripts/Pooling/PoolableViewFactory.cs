using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolableViewFactory
{
    public Transform Container { get; }
    
    readonly IRandomProvider _randomProvider;
    readonly Dictionary<string, ObjectPool<PoolableView>> _pools = new();
    readonly Dictionary<string, MultiObjectPool<PoolableView>> _multiPools = new();

    public PoolableViewFactory(
        Transform container,
        IRandomProvider randomProvider
    )
    {
        Container = container;
        _randomProvider = randomProvider;
    }
    
    public void SetupPool<T> (T prefab) where T : PoolableView
    {
        SetupPool(typeof(T).Name, prefab);
    }

    public void SetupPool<T> (IEnumerable<T> prefabs) where T : PoolableView
    {
        bool isNullOrEmpty = prefabs == null || !prefabs.Any();
        if (isNullOrEmpty)
        {
            DebugUtils.LogException($"Trying to create a multi-pool of {typeof(T).Name} with an empty/null prefab list.");
            return;
        }
        
        if (prefabs.Count() == 1)
        {
            SetupPool(prefabs.First());
            return;
        }
        
        SetupPool(typeof(T).Name, prefabs);
    }

    public void SetupPool<T> (string poolName, T prefab) where T : PoolableView
    {
        _pools.TryAdd(poolName, new ObjectPool<PoolableView>(prefab, this));
    }

    public void SetupPool<T> (string poolName, IEnumerable<T> prefabs) where T : PoolableView
    {
        _multiPools.TryAdd(poolName, new MultiObjectPool<PoolableView>(prefabs, this, _randomProvider));
    }
    
    public T GetView<T> (Transform container) where T : PoolableView
    {
        return GetView<T>(typeof(T).Name, container);
    }

    public T GetView<T> (string poolName, Transform container) where T : PoolableView
    {
        PoolableView view = null;
        
        if (_pools.TryGetValue(poolName, out ObjectPool<PoolableView> pool))
            view = pool.Get();
        else if (_multiPools.TryGetValue(poolName, out MultiObjectPool<PoolableView> multiPool))
            view = multiPool.Get();
        else
        {
            DebugUtils.LogError(
                $"Trying to get a null poolable object. Make sure the pool was setup for {typeof(T).Name}",
                true
            );
            return default;
        }
        
        view.transform.SetParent(container);
        return view as T;
    }
    
    public void ReleaseView (PoolableView view)
    {
        ReleaseView(view.GetType().Name, view);
    }
    
    public void ReleaseView (string poolName, PoolableView view)
    {
        if (_pools.TryGetValue(poolName, out ObjectPool<PoolableView> pool))
        {
            pool.Release(view);
        }
        else if (_multiPools.TryGetValue(poolName, out MultiObjectPool<PoolableView> multiPool))
            multiPool.Release(view);
    }
}