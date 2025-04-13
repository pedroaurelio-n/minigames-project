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

    public T Get ()
    {
        if (_inactiveObjects.Count == 0)
            return Create();

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

    T Create ()
    {
        T obj = GameObject.Instantiate(_prefab);
        obj.Setup(() => _viewFactory.ReleaseView<T>(obj));
        obj.gameObject.SetActive(true);
        _activeObjects.Add(obj);
        return obj;
    }
}