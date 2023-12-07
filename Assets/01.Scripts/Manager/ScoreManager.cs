using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance = null;

    private int _score;
    public int Score => _score;

    [SerializeField] private float _scoreUpDelay = 1f;
    private float _scoreUpTimer = 0f;

    public event Action<int> OnScoreUpEvent = null;

    private void Awake()
    {
        _score = 0;
        _scoreUpTimer = 0f;
    }

    private void Update()
    {
        _scoreUpTimer += Time.deltaTime;
        if (_scoreUpTimer >= _scoreUpDelay)
        {
            _scoreUpTimer = 0f;
            ScoreUp();
        }
    }

    public void ScoreUp(int score = 1)
    {
        _score += score;
        Debug.Log(_score);
        OnScoreUpEvent?.Invoke(_score);
    }
}
