using UnityEngine;

public sealed class InteractiveRockController : MonoBehaviour
{
    [SerializeField] private Transform _rockPosition;
    private bool _isInteractive;

    public void SetInteractive() => _isInteractive = true;
    public void UnsetInteractive() => _isInteractive = false;

    private void Update()
    {
        if (_isInteractive == true)
            return;
        transform.position = _rockPosition.position;
    }
}