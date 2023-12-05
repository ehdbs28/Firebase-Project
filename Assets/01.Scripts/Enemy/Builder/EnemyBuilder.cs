using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilder
{
    private List<EnemyController> _enemies;

    public bool IsEmpty => _enemies.Count <= 0;

    public EnemyBuilder()
    {
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
            var enemy = PoolManager.Instance.Pop("Enemy") as EnemyController;
            enemy.transform.position = spawnPoint;
            _enemies.Add(enemy);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Vector2 GetSpawnPoint()
    {
        var screenSize = new Vector2(Screen.width, Screen.height);
        var randomScreenPos = new Vector2(Random.Range(0f, screenSize.x), Random.Range(0f, screenSize.y));
        var randomWorldPos = GameManager.Instance.MainCam.ScreenToWorldPoint(randomScreenPos);
        randomWorldPos.z = 0;
        return randomWorldPos;
    }
}