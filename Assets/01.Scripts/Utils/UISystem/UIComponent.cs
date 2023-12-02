using UnityEngine;

public abstract class UIComponent : MonoBehaviour, IUI
{
    private Transform _prevParent;

    private Transform _parent;
    public Transform Parent => _parent;

    private UIGenerateOption _options;
    public UIGenerateOption Options => _options;

    private bool _isActive;
    public bool IsActive => _isActive;

    public void GenerateUI(Transform parent, UIGenerateOption options)
    {
        _prevParent = transform.parent;
        _parent = parent;
        _options = options;

        if (options.HasFlag(UIGenerateOption.CLEAR_PANEL))
        {
            // UIManager.Instance.ClearPanel();
        }
        
        transform.SetParent(parent);

        if (options.HasFlag(UIGenerateOption.RESET_POS))
        {
            ((RectTransform)transform).offsetMin = Vector2.zero;
            ((RectTransform)transform).offsetMax = Vector2.zero;
        }

        _isActive = true;
    }

    public void RemoveUI()
    {
        _isActive = false;
        transform.SetParent(_prevParent);
        // PoolManager.Instance.Push(this);
    }

    // public sealed override void Init()
    // {
    //     // Do Nothing;
    // }

    public abstract void UpdateUI();
}