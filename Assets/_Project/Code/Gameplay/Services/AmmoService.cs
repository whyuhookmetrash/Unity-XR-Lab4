using System;
using UnityEngine;

public sealed class AmmoService : MonoBehaviour
{
    public event Action<int> OnAmmoChanged;

    public int CurrentAmmo => _currentAmmo;
    [SerializeField] private int _currentAmmo;
    
    public bool TrySpendAmmo(int ammo)
    {
        if (_currentAmmo >= ammo)
        {
            _currentAmmo -= ammo;
            OnAmmoChanged?.Invoke(_currentAmmo);
            return true;
        }
        return false;
    }

    public void ReplenishAmmo(int ammo)
    {
        _currentAmmo += ammo;
        OnAmmoChanged?.Invoke(_currentAmmo);
    }

    public void SetAmmo(int ammo)
    {
        _currentAmmo = ammo;
        OnAmmoChanged?.Invoke(_currentAmmo);
    }
}