using UnityEngine;

public abstract class BaseModule<T> : IModule where T : ModuleController
{
    public T Controller { get; private set; }

    protected BaseModule(T controller)
    {
        Controller = controller;
        Controller.OnUpdatingEvent += UpdateModule;
        Controller.OnFixedUpdatingEvent += FixedUpdateModule;
    }
    
    ~BaseModule()
    {
        Controller.OnUpdatingEvent -= UpdateModule;
        Controller.OnFixedUpdatingEvent -= FixedUpdateModule;
    }
    
    public abstract void UpdateModule();
    public abstract void FixedUpdateModule();
}