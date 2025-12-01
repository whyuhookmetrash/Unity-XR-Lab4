using UnityEngine;

public sealed class ScoreVFXFactory : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _vfxDistance;

    private MemoryPoolService _memoryPoolService;

    private void Awake()
    {
        _memoryPoolService = GameObject.FindFirstObjectByType<MemoryPoolService>();
    }

    public void SpawnVFX(int score, Vector3 position)
    {
        var playerPosition = _player.transform.position;
        Vector3 directionToPlayer = (playerPosition - position).normalized;
        Vector3 targetPosition = playerPosition - directionToPlayer * _vfxDistance;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        var vfx = _memoryPoolService.SpawnItem<ScoreVFX>(MemoryPoolId.ScoreVFX);
        vfx.InitAndPlayAnaimation(
            _memoryPoolService,
            targetPosition,
            targetRotation,
            score.ToString());
    }
}