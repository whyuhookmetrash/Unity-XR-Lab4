using UnityEngine;

public sealed class Weapon : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifetime;
    [SerializeField] private int _ammoPerShoot;
    [SerializeField] private float _bulletForce;
    [SerializeField] private MemoryPoolId _bulletPrefabId;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private AudioClip _fireClip;

    private AudioSource _audioSource;
    private MemoryPoolService _memoryPoolService;
    private AmmoService _ammoService;

    private void Awake()
    {
        _memoryPoolService = GameObject.FindFirstObjectByType<MemoryPoolService>();
        _ammoService = GameObject.FindFirstObjectByType<AmmoService>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void FireBullet()
    {
        if (_ammoService.TrySpendAmmo(_ammoPerShoot) == false)
            return;

        var bullet = _memoryPoolService.SpawnItem<Bullet>(MemoryPoolId.Bullet);
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_fireClip);
        bullet.Init(
            _memoryPoolService,
            _firePoint.forward,
            _firePoint.rotation,
            _firePoint.position,
            _bulletSpeed,
            _bulletLifetime,
            _bulletForce);

        CheckOnTarget();
    }

    private void CheckOnTarget()
    {
        int targetLayer = LayerMask.NameToLayer("Target");
        int staticLayer = LayerMask.NameToLayer("Static");
        LayerMask layerMask = (1 << targetLayer) | (1 << staticLayer);

        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out RaycastHit hit, 50f, layerMask))
        {
            if (hit.collider.CompareTag("ArcheryTarget"))
            {
                var hitPoint = hit.point;
                hit.collider.gameObject.GetComponent<ScoreComponent>().CountCenterRecalculate(hitPoint);
            }
        }
    }
}
