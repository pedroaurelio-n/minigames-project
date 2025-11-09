using System.Collections.Generic;
using UnityEngine;
using Enumerable = System.Linq.Enumerable;

public class ObjectPool<T> where T : PoolableView
{
    readonly HashSet<T> _activeObjects = new();
    readonly HashSet<T> _inactiveObjects = new();
    readonly T _prefab;
    readonly PoolableViewFactory _viewFactory;

    public ObjectPool (
        T prefab,
        PoolableViewFactory viewFactory
    )
    {
        _prefab = prefab;
        _viewFactory = viewFactory;
    }

    public T Get (int poolIndex = 0)
    {
        if (_inactiveObjects.Count == 0)
            return Create(poolIndex);

        T obj = Enumerable.First(_inactiveObjects);
        obj.gameObject.SetActive(true);
        _inactiveObjects.Remove(obj);
        _activeObjects.Add(obj);
        return obj;
    }

    public void Release (T obj)
    {
        if (!_activeObjects.Contains(obj))
            return;

        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_viewFactory.Container);
        _activeObjects.Remove(obj);
        _inactiveObjects.Add(obj);
    }

    T Create (int poolIndex)
    {
        T obj = Object.Instantiate(_prefab);
        obj.Setup(() => _viewFactory.ReleaseView(obj), poolIndex);
        obj.gameObject.SetActive(true);
        _activeObjects.Add(obj);
        return obj;
    }
}