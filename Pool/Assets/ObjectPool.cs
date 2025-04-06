using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _inactiveObjects = new Queue<T>();
    private readonly List<T> _activeObjects = new List<T>();
    private readonly int _maxPoolSize;
    private int _totalCreated;

    public int ActiveCount => _activeObjects.Count;
    public int InactiveCount => _inactiveObjects.Count;
    public int TotalCreated => _totalCreated;

    public ObjectPool(T prefab, int startPoolSize, int maxPoolSize, Transform parent = null)
    {
        _prefab = prefab;
        _maxPoolSize = maxPoolSize;
        _parent = parent;
        InitializePool(startPoolSize);
    }

    private void InitializePool(int startPoolSize)
    {
        int objectsToCreate = Mathf.Min(startPoolSize, _maxPoolSize);
        for (int i = 0; i < objectsToCreate; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject()
    {
        T newObj = Object.Instantiate(_prefab, _parent);
        newObj.gameObject.SetActive(false);
        _inactiveObjects.Enqueue(newObj);
        _totalCreated++; 
        return newObj;
    }

    public T TryGetFromPool()
    {
        
        if (_inactiveObjects.Count == 0 && _totalCreated < _maxPoolSize)
        {
            CreateObject();
        }

        if (_inactiveObjects.Count > 0)
        {
            T obj = _inactiveObjects.Dequeue();
            _activeObjects.Add(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        Debug.LogWarning($"Pool limit reached! Max size: {_maxPoolSize}");
        return null;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _activeObjects.Remove(obj);
        _inactiveObjects.Enqueue(obj);
    }

    internal Bullet GetFromPool()
    {
        throw new System.NotImplementedException();
    }
}