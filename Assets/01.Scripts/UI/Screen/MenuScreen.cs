using UnityEngine;

public class MenuScreen : UIComponent
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

    public void PlayGameHandle()
    {
        UIManager.Instance.GenerateUI("InGameScreen");
        StageManager.Instance.EnableStage();
    }

    public void LeaderBoardHandle()
    {
        UIManager.Instance.GenerateUI("LeaderBoardScreen");
    }

    public void SettingHandle()
    {
        UIManager.Instance.GenerateUI("SettingScreen");
    }

    public void QuitHandle()
    {
        Application.Quit();
    }
}