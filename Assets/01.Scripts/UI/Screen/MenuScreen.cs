using TMPro;
using UnityEngine;

public class MenuScreen : UIComponent
{
    [SerializeField] private UIMenuSystem _menuSystem;
    [SerializeField] private TextMeshProUGUI _dayCountText;

    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        _menuSystem.SetUp(this);
        _dayCountText.text = $"{AuthManager.Instance.DayCount.ToString()}일째 연속출석중";
    }

    public override void RemoveUI()
    {
        base.RemoveUI();
        _menuSystem.Release();
    }

    public void PlayGameHandle()
    {
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