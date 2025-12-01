using DG.Tweening;
using TMPro;
using UnityEngine;

public sealed class ScoreVFX : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private float _scaleDuration;
    [SerializeField] private float _moveDuration;

    [SerializeField] private Transform _startPosition;
    [SerializeField] private Transform _endPosition;

    public void InitAndPlayAnaimation(
        MemoryPoolService memoryPoolService,
        Vector3 position,
        Quaternion rotation,
        string scoreText)
    {
        _scoreText.text = scoreText;
        _transform.SetPositionAndRotation(position, rotation);

        var transform = _scoreText.transform;
        transform.localScale = Vector3.zero;
        transform.position = _startPosition.position;

        transform.DOScale(Vector3.one, _scaleDuration);
        transform.DOMove(_endPosition.position, _moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => transform.DOScale(Vector3.zero, _scaleDuration)
            .OnComplete(() => memoryPoolService.UnspawnItem(MemoryPoolId.ScoreVFX, this)));
    }
}