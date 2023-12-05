using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    private int _stage;

    private EnemyBuilder _enemyEnemyBuilder;
    public EnemyBuilder EnemyBuilder => _enemyEnemyBuilder;

    private ItemBuilder _itemBuilder;
    public ItemBuilder ItemBuilder => _itemBuilder;

    [SerializeField] private List<BuildArea> _enemyBuildAreas;
    [SerializeField] private BuildArea _itemBuildArea;

    private void Awake()
    {
        _stage = 1;
        _enemyEnemyBuilder = new EnemyBuilder(_enemyBuildAreas);
        _itemBuilder = new ItemBuilder(_itemBuildArea);
    }

    private void Start()
    {
        _itemBuilder.SpawnItem();
    }

    private void Update()
    {
        if (_enemyEnemyBuilder.IsEmpty)
        {
            _enemyEnemyBuilder.SpawnNewStage(_stage++);
        }
    }
}