using UnityEngine;

public sealed class RockController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _lifeTime;
    [SerializeField] private Vector3 _customGravity = new Vector3(0, -2.45f, 0);

    private float _currentTime;

    private void FixedUpdate()
    {
        _rb.AddForce(_customGravity, ForceMode.Acceleration);

        _currentTime += Time.fixedDeltaTime;
        if (_currentTime >= _lifeTime)
        {
            Destroy(this);
        }
    }
}