using UnityEngine;
using UnityEngine.Events;

public class UIMenu : PoolableMono
{
    private float _indexPosition;
    public float IndexPosition => _indexPosition;

    public UnityEvent CallBackEvent = null;

    public void SetUp()
    {
        Canvas.ForceUpdateCanvases();
        _indexPosition = ((RectTransform)transform).anchoredPosition.y;
    }

    public virtual void OnAction()
    {
        CallBackEvent?.Invoke();
    }

    public override void Init()
    {
    }
}