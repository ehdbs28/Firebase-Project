using System;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuSystem : MonoBehaviour
{
    [SerializeField] private InputControl _inputSO;

    private RectTransform _cursorRectTransform;

    private List<UIMenu> _menus;

    private UIComponent _ownerComponent;

    private int _index;
    public int Index => _index;

    public void SetUp(UIComponent ownerComponent)
    {
        _menus = new List<UIMenu>();
        GetComponentsInChildren(_menus);
        _menus.ForEach(menu => menu.SetUp());

        _ownerComponent = ownerComponent;

        _cursorRectTransform = (RectTransform)transform.Find("Cursor");

        _inputSO.OnMenuUpEvent += IndexUp;
        _inputSO.OnMenuDownEvent += IndexDown;
        _inputSO.OnSelectEvent += ActionCallBack;
        
        SetIndex(0);
    }

    public void Release()
    {
        _inputSO.OnMenuUpEvent -= IndexUp;
        _inputSO.OnMenuDownEvent -= IndexDown;
        _inputSO.OnSelectEvent -= ActionCallBack;
    }

    private void IndexUp()
    {
        if (UIManager.Instance.TopComponent != _ownerComponent)
        {
            return;
        }

        SetIndex(_index - 1);
    }

    private void IndexDown()
    {
        if (UIManager.Instance.TopComponent != _ownerComponent)
        {
            return;
        }
        
        SetIndex(_index + 1);
    }

    private void SetIndex(int index)
    {
        _index = index;
        
        if (_index < 0)
        {
            _index = _menus.Count - 1;
        }

        if (_index >= _menus.Count)
        {
            _index = 0;
        }
        
        _cursorRectTransform.anchoredPosition = new Vector2(_cursorRectTransform.anchoredPosition.x, _menus[_index].IndexPosition);
    }

    private void ActionCallBack()
    {
        if (UIManager.Instance.TopComponent != _ownerComponent)
        {
            return;
        }
        
        _menus[_index].OnAction();
    }
}