using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    private int _stage;

    private EnemyBuilder _enemyEnemyBuilder;
    public EnemyBuilder EnemyBuilder => _enemyEnemyBuilder;

    private ItemBuilder _itemBuilder;
    public ItemBuilder ItemBuilder => _itemBuilder;

    private SnakeController _snake;
    public SnakeController Snake => _snake;

    [SerializeField] private List<BuildArea> _enemyBuildAreas;
    [SerializeField] private BuildArea _itemBuildArea;

    private bool _onStage;

    private void Awake()
    {
        _enemyEnemyBuilder = new EnemyBuilder(_enemyBuildAreas);
        _itemBuilder = new ItemBuilder(_itemBuildArea);
        _snake = null;
    }

    private void Update()
    {
        if (_onStage && _enemyEnemyBuilder.IsEmpty)
        {
            _enemyEnemyBuilder.SpawnNewStage(_stage++);
        }
    }

    public void EnableStage()
    {
        UIManager.Instance.GenerateUI("InGameScreen");
        _stage = 1;
        ScoreManager.Instance.ResetScore();
        _snake = PoolManager.Instance.Pop("Snake") as SnakeController;
        _itemBuilder.SpawnItem();
        _onStage = true;
    }

    public void DisableStage()
    {
        _onStage = false;
        _itemBuilder.RemoveItem();
        _enemyEnemyBuilder.RemoveAllEnemy();
        PoolManager.Instance.Push(_snake);
        _snake = null;
        AuthManager.Instance.SetScore(ScoreManager.Instance.Score);
        UIManager.Instance.GenerateUI("ResultScreen");
    }
}