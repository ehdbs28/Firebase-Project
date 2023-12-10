using TMPro;
using UnityEngine;

public class PlayerCard : UIMenu
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    private string _id;

    public void Setting(string name, string id, int score)
    {
        _id = id;
        _nameText.text = name;
        _scoreText.text = $"({score})";
    }

    public override void OnAction()
    {
        AuthManager.Instance.SetFriend(AuthManager.Instance.User.UserId, _id);
    }
}