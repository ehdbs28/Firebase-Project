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

    private async void EnterKeyHandle()
    {
        var email = _emailInput.text;
        var password = _passwordInput.text;

        if (await AuthManager.Instance.SignIn(email, password))
        {
            UIManager.Instance.GenerateUI("MenuScreen");
        }
    }
}