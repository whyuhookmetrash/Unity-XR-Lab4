using UnityEngine;
using UnityEngine.UIElements;

public sealed class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _collider;

    private float _force;
    private float _lifeTime;
    private float _currentLifeTime;
    private Vector3 _direction;
    private MemoryPoolService _memoryPoolService;
    private bool _isActive;

    public void Init(
        MemoryPoolService memoryPoolService,
        Vector3 direction,
        Quaternion rotation,
        Vector3 position,
        float speed,
        float lifeTime,
        float force)
    {
        _memoryPoolService = memoryPoolService;
        _lifeTime = lifeTime;
        _currentLifeTime = 0f;
        _direction = direction;
        _isActive = true;
        _force = force;
        transform.SetPositionAndRotation(position, rotation);
        _rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject;
        var layer = target.layer;
        if (layer == 11) //Static
        {
            Dispose();
        }
        else if (layer == 10) //Target
        {
            if(target.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddForceAtPosition(_direction * _force, transform.position, ForceMode.Impulse);
            }
            if (target.TryGetComponent<ScoreComponent>(out var score))
            {
                score.CountScore();
            }
            Dispose();
        }
    }

    private void Update()
    {
        if (_isActive == false)
            return;

        _currentLifeTime += Time.deltaTime;
        if (_currentLifeTime >= _lifeTime)
            Dispose();
    }

    public void Dispose()
    {
        _isActive = false;
        _memoryPoolService.UnspawnItem(MemoryPoolId.Bullet, this);
    }
}