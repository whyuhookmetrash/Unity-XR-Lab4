using System.Linq;
using UnityEngine;

public sealed class ScoreComponent : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private int _maxHardness;
    [SerializeField] private bool _isInvulnerable;
    [SerializeField] private bool _isCentreRecalculate;
    [SerializeField] private float _recalculateRadius;
    [SerializeField] private bool _isGroundScored;
    [SerializeField] private Outline _outline;
    [SerializeField] private Transform _center;
    private bool _isScored;
    private int _currentHardness;

    private ScoreService _scoreService;
    private ScoreVFXFactory _scoreVFXFactory;
    private MemoryPoolService _memoryPoolService;

    private void Awake()
    {
        _scoreService = FindFirstObjectByType<ScoreService>();
        _scoreVFXFactory = FindFirstObjectByType<ScoreVFXFactory>();
        _memoryPoolService = FindFirstObjectByType<MemoryPoolService>();
        Init();
    }

    public void Init()
    {
        _isScored = false;
        _currentHardness = 0;
        _outline.OutlineWidth = 5f;
    }

    public void CountScore()
    {
        if (_isScored == true || _isCentreRecalculate == true)
            return;

        CheckHardness();
        _scoreService.AddScore(_score);
        _scoreVFXFactory.SpawnVFX(_score, transform.position);        
    }

    public void CountCenterRecalculate(Vector3 hitPosition)
    {
        if (_isScored == true)
            return;

        CheckHardness();
        if (_isCentreRecalculate)
        {
            var center = _center.position;
            var radius = _recalculateRadius;
            float distance = Vector3.Distance(hitPosition, center);
            float normalized = 1f - (distance / radius);
            var score = Mathf.Max(0, _score * normalized);
            var scoreInt = (int)score;
            _scoreService.AddScore(scoreInt);
            _scoreVFXFactory.SpawnVFX(scoreInt, hitPosition);
            var bulletMark = _memoryPoolService.SpawnItem(MemoryPoolId.BulletMark);
            bulletMark.transform.position = hitPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (_isGroundScored == true && _isScored == false)
            {
                _scoreService.AddScore(_score);
                _scoreVFXFactory.SpawnVFX(_score, transform.position);
                RemoveOutline();
                _isScored = true;
            }
        }
    }

    private void CheckHardness()
    {
        if (_isInvulnerable == false)
        {
            _currentHardness += 1;
            if (_currentHardness >= _maxHardness)
            {
                RemoveOutline();
                _isScored = true;
            }
        }
    }

    private void RemoveOutline()
    {
        _outline.OutlineWidth = 0f;
    }
}