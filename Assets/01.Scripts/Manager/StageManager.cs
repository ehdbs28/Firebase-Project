using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance = null;

    private int _stage;

    private EnemyBuilder _enemyBuilder;

    private void Awake()
    {
        _stage = 1;
        _enemyBuilder = new EnemyBuilder();
    }

    private void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            _enemyBuilder.SpawnNewStage(_stage++);
        }
    }
}