using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class MemoryPoolService : MonoBehaviour
{
    private readonly Dictionary<MemoryPoolId, MemoryPool> _memoryPools = new();

    [SerializeField] private MemoryPoolData[] _memoryPoolData;

    public void Awake()
    {
        for (int i = 0; i < _memoryPoolData.Length; i++)
        {
            var data = _memoryPoolData[i];
            _memoryPools.Add(data.Id, new MemoryPool(data.Prefab, data.Container, data.InitCount));
        }
    }

    public void Start()
    {
        foreach (var pool in _memoryPools.Values)
        {
            pool.Init();
        }
    }

    public GameObject SpawnItem(MemoryPoolId id)
    {
        return _memoryPools[id].SpawnItem();
    }

    public T SpawnItem<T>(MemoryPoolId id) where T : Component
    {
        return _memoryPools[id].SpawnItem().GetComponent<T>();
    }

    public void UnspawnItem(MemoryPoolId id, GameObject item)
    {
        _memoryPools[id].UnspawnItem(item);
    }

    public void UnspawnItem<T>(MemoryPoolId id, T item) where T : Component
    {
        _memoryPools[id].UnspawnItem(item.gameObject);
    }

    private void OnDestroy()
    {
        foreach (var pool in _memoryPools.Values)
        {
            pool.Dispose();
        }
    }
}