using TMPro;
using UnityEngine;

public class LoginScreen : UIComponent
{
    [SerializeField] private InputControl _inputControl;
    
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;

    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        _inputControl.OnSelectEvent += EnterKeyHandle;
    }

    public override void RemoveUI()
    {
        base.RemoveUI();
        _inputControl.OnSelectEvent -= EnterKeyHandle;
    }

    private void EnterKeyHandle()
    {
        UIManager.Instance.GenerateUI("MenuScreen");
    }
}