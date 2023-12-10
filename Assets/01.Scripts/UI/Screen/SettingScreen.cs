using UnityEngine;

public class SettingScreen : UIComponent
{
    [SerializeField] private UIMenuSystem _menuSystem;
    [SerializeField] private InputControl _inputControl;

    [SerializeField] private RectTransform[] _cursors;

    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        _menuSystem.SetUp(this);
        _inputControl.OnSelectEvent += SelectHandle;
        _inputControl.OnRightEvent += VolumeUp;
        _inputControl.OnLeftEvent += VolumeDown;
    }

    public override void RemoveUI()
    {
        base.RemoveUI();
        _menuSystem.Release();
        _inputControl.OnSelectEvent -= SelectHandle;
        _inputControl.OnRightEvent -= VolumeUp;
        _inputControl.OnLeftEvent -= VolumeDown;
    }

    private void SelectHandle()
    {
        UIManager.Instance.ReturnUI();
    }

    private void VolumeUp()
    {
        if (_menuSystem.Index == 0)
        {
            
        }
        else
        {
            
        }
    }

    private void VolumeDown()
    {
        if (_menuSystem.Index == 0)
        {
            
        }
        else
        {
            
        }
    }
}