using System;
using UnityEngine;

public sealed class RockService : MonoBehaviour
{
    public event Action<int> OnRockChanged;

    public int CurrentRock => _currentRock;
    [SerializeField] private int _currentRock;

    public bool TrySpendRock(int ammo)
    {
        if (_currentRock >= ammo)
        {
            _currentRock -= ammo;
            OnRockChanged?.Invoke(_currentRock);
            return true;
        }
        return false;
    }

    public void ReplenishRock(int ammo)
    {
        _currentRock += ammo;
        OnRockChanged?.Invoke(_currentRock);
    }

    public void SetRock(int ammo)
    {
        _currentRock = ammo;
        OnRockChanged?.Invoke(_currentRock);
    }
}