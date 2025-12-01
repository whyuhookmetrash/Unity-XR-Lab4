using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine;

public sealed class MemoryPool
{
    private readonly GameObject _prefab;
    private readonly Transform _container;
    private readonly int _initCount;

    private readonly List<GameObject> _activeItems = new();
    private readonly Queue<GameObject> _freeItems = new();

    public MemoryPool(
        GameObject prefab,
        Transform container,
        int initCount)
    {
        _prefab = prefab;
        _container = container;
        _initCount = initCount;
    }

    public void Init()
    {
        for (int i = 0; i < _initCount; i++)
        {
            var item = Object.Instantiate(_prefab, _container);
            _freeItems.Enqueue(item);
            item.SetActive(false);
        }
    }

    public GameObject SpawnItem()
    {
        if (_freeItems.TryDequeue(out var item))
        {
            _activeItems.Add(item);
            item.SetActive(true);
            return item;
        }
        else
        {
            item = Object.Instantiate(_prefab, _container);
            _activeItems.Add(item);
            return item;
        }
    }

    public void UnspawnItem(GameObject item)
    {
        item.SetActive(false);
        _activeItems.Remove(item);
        _freeItems.Enqueue(item);
    }

    public void Dispose()
    {
        for (int i = 0; i < _activeItems.Count; i++)
        {
            Object.Destroy(_activeItems[i]);
        }
        while (_freeItems.TryDequeue(out var item))
        {
            Object.Destroy(item);
        }
        _activeItems.Clear();
    }
}