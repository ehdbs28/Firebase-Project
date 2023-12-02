using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private Stack<PoolableMono> _pool;

    private PoolableMono _prefab;
    private Transform _parent;

    public Pool(PoolableMono prefab, Transform parent, int cnt)
    {
        _pool = new Stack<PoolableMono>();
        
        _prefab = prefab;
        _parent = parent;

        for (var i = 0; i < cnt; i++)
        {
            var obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }   
    }

    public PoolableMono Pop()
    {
        PoolableMono obj = null;
        if (_pool.Count > 0)
        {
            obj = _pool.Pop();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        return obj;
    }

    public void Push(PoolableMono obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}