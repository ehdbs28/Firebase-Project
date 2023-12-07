using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilder
{
    private readonly List<EnemyController> _enemies;
    private readonly List<BuildArea> _buildAreas;

    public List<EnemyController> Enemies => _enemies;
    public bool IsEmpty => _enemies.Count <= 0;

    public EnemyBuilder(List<BuildArea> buildAreas)
    {
        _buildAreas = buildAreas;
        _enemies = new List<EnemyController>();
    }
    
    public void SpawnNewStage(int stage)
    {
        StageManager.Instance.StartCoroutine(SpawnRoutine(stage));
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        _enemies.Remove(enemy);
    }

    private IEnumerator SpawnRoutine(int stage)
    {
        var cnt = Mathf.Floor(Mathf.Pow(stage * (30f / 29f), 1.05f));
        for (var i = 0; i < cnt; i++)
        {
            var spawnPoint = GetSpawnPoint();

            var effect = PoolManager.Instance.Pop("GenerateEnemyEffect") as PoolableParticle;
            effect.SetPositionAndRotation(spawnPoint);
            effect.Play();

            var enemy = PoolManager.Instance.Pop("Enemy") as EnemyController;
            enemy.transform.position = spawnPoint;
            _enemies.Add(enemy);
            
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Vector2 GetSpawnPoint()
    {
        var randIdx = Random.Range(0, _buildAreas.Count);
        return _buildAreas[randIdx].GetAreaPosition();
    }
}