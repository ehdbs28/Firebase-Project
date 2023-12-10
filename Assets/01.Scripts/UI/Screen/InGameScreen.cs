using TMPro;
using UnityEngine;

public class InGameScreen : UIComponent
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        ScoreManager.Instance.OnScoreUpEvent += ScoreUpHandle;
    }

    private void ScoreUpHandle(int score)
    {
        _scoreText.text = score.ToString();
    }
}