using UnityEngine;

public class TitleScreen : UIComponent
{
    [SerializeField] private InputControl _inputControl;

    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        _inputControl.OnAnyKeyEvent += AnyKeyHandle;
    }

    public override void RemoveUI()
    {
        base.RemoveUI();
        _inputControl.OnAnyKeyEvent -= AnyKeyHandle;
    }

    private void AnyKeyHandle()
    {
        UIManager.Instance.GenerateUI("SignScreen",
            UIGenerateOption.CLEAR_PANEL | UIGenerateOption.RESET_POS | UIGenerateOption.STACKING);
    }
}