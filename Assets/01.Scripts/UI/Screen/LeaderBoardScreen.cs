using UnityEngine;

public class LeaderBoardScreen : UIComponent
{
    [SerializeField] private InputControl _inputControl;
    
    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        _inputControl.OnSelectEvent += EnterHandle;
    }

    public override void RemoveUI()
    {
        base.RemoveUI();
        _inputControl.OnSelectEvent -= EnterHandle;
    }

    private void EnterHandle()
    {
        UIManager.Instance.ReturnUI();    
    }
}