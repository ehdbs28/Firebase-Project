using UnityEngine;

public interface IUI
{
    public void GenerateUI(Transform parent, UIGenerateOption options);
    public void RemoveUI();
    public void UpdateUI();
}