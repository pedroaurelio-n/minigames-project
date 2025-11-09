using System.Collections.Generic;
using UnityEngine;

public class MultiObjectPool<T> where T : PoolableView
{
    readonly PoolableViewFactory _viewFactory;
    readonly IRandomProvider _randomProvider;
    readonly Dictionary<int, ObjectPool<T>> _subPools = new();

    int _index;

    public MultiObjectPool (
        IEnumerable<T> prefabs,
        PoolableViewFactory viewFactory,
        IRandomProvider randomProvider
    )
    {
        foreach (T prefab in prefabs)
        {
            _subPools.TryAdd(_index, new ObjectPool<T>(prefab, viewFactory));
            _index++;
        }
        _viewFactory = viewFactory;
        _randomProvider = randomProvider;
    }
    
    public T Get ()
    {
        int randomIndex = _randomProvider.Range(0, _index);
        ObjectPool<T> chosenPool = _subPools[randomIndex];
        
        return chosenPool.Get(randomIndex);
    }

    public void Release (T obj)
    {
        _subPools[obj.PoolIndex].Release(obj);
    }
}