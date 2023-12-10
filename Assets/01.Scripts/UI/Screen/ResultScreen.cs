using TMPro;
using UnityEngine;

public class ResultScreen : UIComponent
{
    [SerializeField] private InputControl _inputControl;

    [SerializeField] private TextMeshProUGUI _scoreText;
    
    public override void GenerateUI(Transform parent, UIGenerateOption options)
    {
        base.GenerateUI(parent, options);
        _scoreText.text = ScoreManager.Instance.Score.ToString();
        _inputControl.OnResetEvent += ResetHandle;
        _inputControl.OnSelectEvent += EnterHandle;
    }

    public override void RemoveUI()
    {
        base.RemoveUI();
        _inputControl.OnResetEvent -= ResetHandle;
        _inputControl.OnSelectEvent -= EnterHandle;
    }

    private void ResetHandle()
    {
        StageManager.Instance.EnableStage();
    }

    private void EnterHandle()
    {
        UIManager.Instance.GenerateUI("MenuScreen");
    }
}