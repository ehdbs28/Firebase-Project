using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance = null;

    private Dictionary<string, Pool> _pools;

    public PoolManager()
    {
        _pools = new Dictionary<string, Pool>();
    }

    public void CreatePool(PoolableMono prefab, Transform parent, int cnt)
    {
        var pool = new Pool(prefab, parent, cnt);
        _pools.Add(prefab.name, pool);
    }

    public PoolableMono Pop(string name)
    {
        if (!_pools.ContainsKey(name))
        {
            Debug.LogError($"Prefab does not exist on pool : {name}");
            return null;
        }
        
        var obj = _pools[name].Pop();
        obj.Init();
        return obj;
    }

    public void Push(PoolableMono obj)
    {
        _pools[obj.name].Push(obj);
    }
}