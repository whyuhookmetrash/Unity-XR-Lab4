using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public sealed class SlingShot : MonoBehaviour
{
    [SerializeField] private Transform _interactiveRock;
    [SerializeField] private Material _interactiveRockMaterial;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private XRGrabInteractable _interactable;
    [SerializeField] private int _maxRockForce;

    private Vector3 _startPosition;
    private RockService _rockService;
    private MemoryPoolService _memoryPoolService;

    private void Awake()
    {
        _memoryPoolService = FindFirstObjectByType<MemoryPoolService>();
        _rockService = FindFirstObjectByType<RockService>();
        _interactiveRockMaterial.color = Color.white;
        _startPosition = _interactiveRock.localPosition;
        _interactable.enabled = true;
    }

    public void ShootRock()
    {
        if (_rockService.TrySpendRock(1))
        {
            var rock = _memoryPoolService.SpawnItem(MemoryPoolId.Rock);
            var rockRb = rock.GetComponent<Rigidbody>();

            var targetPosition = _interactiveRock.position;
            rock.transform.position = targetPosition;
            var shootPosition = _shootPosition.position;
            var direction = shootPosition - targetPosition;
            var targetDirection = direction.normalized;
            var force = (direction.magnitude / 0.35f) * _maxRockForce;
            rockRb.AddForce(targetDirection * force, ForceMode.Impulse);

            _interactable.enabled = false;
            _interactiveRock.localPosition = _startPosition;
            _interactiveRockMaterial.color = new Color(1f, 1f, 1f, 0f);

            if (_rockService.CurrentRock <= 0)
                return;

            _interactiveRockMaterial.DOColor(Color.white, 1f).SetDelay(1f).OnComplete(() => { _interactable.enabled = true; });
        }
    }
}