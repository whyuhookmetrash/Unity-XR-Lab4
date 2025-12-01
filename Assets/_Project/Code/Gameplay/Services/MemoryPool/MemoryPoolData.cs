using System;
using UnityEngine;

[Serializable]
public sealed class MemoryPoolData
{
    public MemoryPoolId Id;
    public GameObject Prefab;
    public Transform Container;
    public int InitCount;
}