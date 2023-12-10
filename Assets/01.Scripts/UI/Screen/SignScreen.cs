using UnityEngine;

public class SignScreen : UIComponent
{
    [SerializeField] private UIMenuSystem _menuSystem;

    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        _menuSystem.SetUp(this);
    }

    public override void RemoveUI()
    {
        base.RemoveUI();
        _menuSystem.Release();
    }

    public void SignUpHandle()
    {
        UIManager.Instance.GenerateUI("SignUpScreen",
            UIGenerateOption.RESET_POS | UIGenerateOption.STACKING | UIGenerateOption.CLEAR_PANEL);
    }

    public void LoginHandle()
    {
        UIManager.Instance.GenerateUI("LoginScreen",
            UIGenerateOption.RESET_POS | UIGenerateOption.STACKING | UIGenerateOption.CLEAR_PANEL);
    }
}