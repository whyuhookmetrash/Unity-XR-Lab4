using UnityEngine;

public sealed class WeaponLaser : MonoBehaviour
{
    [SerializeField] private DebugRayVisualizer _rayVisualizer;
    [SerializeField] private LineRenderer _lineRenderer;

    public void EnableRay()
    {
        _rayVisualizer.enabled = true;
        _lineRenderer.enabled = true;
    }

    public void DisableRay()
    {
        _rayVisualizer.enabled = false;
        _lineRenderer.enabled = false;
    }
}