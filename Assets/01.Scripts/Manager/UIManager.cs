using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField] private Canvas _mainCanvas;
    public Canvas MainCanvas => _mainCanvas;

    private readonly Stack<UIComponent> _componentStack;
    public UIComponent TopComponent => _componentStack.Peek();

    public UIManager()
    {
        _componentStack = new Stack<UIComponent>();
    }

    public UIComponent GenerateUI(string name, UIGenerateOption options, Transform parent = null)
    {
        if (parent == null)
        {
            parent = _mainCanvas.transform;
        }

        var ui = PoolManager.Instance.Pop(name) as UIComponent;

        if (ui == null)
        {
            return null;
        }
        
        ui.GenerateUI(parent, options);

        if (options.HasFlag(UIGenerateOption.STACKING))
        {
            _componentStack.Push(ui);
        }

        return ui;
    }

    public void RemoveTopUI()
    {
        var top = _componentStack.Pop();
        top.RemoveUI();
    }

    public void ReturnUI()
    {
        if (_componentStack.Count <= 0)
        {
            return;
        }

        var current = _componentStack.Pop();

        if (_componentStack.Count <= 0)
        {
            return;
        }

        var prev = _componentStack.Pop();
        
        current.RemoveUI();
        GenerateUI(prev.name, prev.Options, prev.Parent);
    }

    public void ClearPanel()
    {
        var components = new List<UIComponent>();
        _mainCanvas.GetComponentsInChildren(components);

        foreach (var compo in components)
        {
            compo.RemoveUI();
        }
    }
}