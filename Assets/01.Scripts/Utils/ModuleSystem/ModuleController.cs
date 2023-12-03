using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModuleController : MonoBehaviour
{
    public event Action OnUpdatingEvent = null;
    public event Action OnFixedUpdatingEvent = null;
    public event Action OnReleaseEvent = null;

    private readonly List<IModule> _modules = new();

    public virtual void Update()
    {
        OnUpdatingEvent?.Invoke();
    }

    public virtual void FixedUpdate()
    {
        OnFixedUpdatingEvent?.Invoke();
    }

    public virtual void OnDisable()
    {
        OnReleaseEvent?.Invoke();
    }

    public void AddModule(IModule module)
    {
        _modules.Add(module);
    }

    public T GetModule<T>() where T : IModule
    {
        return _modules.OfType<T>().First();
    }
}