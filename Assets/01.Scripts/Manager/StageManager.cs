using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    private int _stage;

    private EnemyBuilder _enemyBuilder;
    public EnemyBuilder Builder => _enemyBuilder;

    [SerializeField] private List<BuildArea> _buildAreas;

    private void Awake()
    {
        _stage = 1;
        _enemyBuilder = new EnemyBuilder(_buildAreas);
    }

    private void Update()
    {
        if (_enemyBuilder.IsEmpty)
        {
            _enemyBuilder.SpawnNewStage(_stage++);
        }
    }
}