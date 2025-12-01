using System;
using UnityEngine;

public sealed class ScoreService : MonoBehaviour
{
    public event Action<int> OnScoreChanged;

    public int CurrentScore => _currentScore;
    private int _currentScore = 0;

    public void AddScore(int score)
    {
        _currentScore += score;
        OnScoreChanged?.Invoke(_currentScore);
    }

    public void SetScore(int score)
    {
        _currentScore = score;
        OnScoreChanged?.Invoke(_currentScore);
    }
}