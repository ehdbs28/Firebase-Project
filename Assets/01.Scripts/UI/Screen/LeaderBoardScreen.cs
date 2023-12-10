using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardScreen : UIComponent
{
    [SerializeField] private InputControl _inputControl;
    [SerializeField] private UIMenuSystem _menuSystem;
    
    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        _inputControl.OnBackEvent += EnterHandle;
        
        foreach (var pair in AuthManager.Instance.Users)
        {
            var playerCard = PoolManager.Instance.Pop("PlayerCard") as PlayerCard;
            playerCard.Setting(pair.Value.name, pair.Key, pair.Value.score);
            playerCard.transform.SetParent(_menuSystem.MenuTrm);
        }
        _menuSystem.SetUp(this);
    }

    public override void RemoveUI()
    {
        base.RemoveUI();
        _inputControl.OnBackEvent -= EnterHandle;
        _menuSystem.Release();
    }

    private void EnterHandle()
    {
        UIManager.Instance.ReturnUI();    
    }
}