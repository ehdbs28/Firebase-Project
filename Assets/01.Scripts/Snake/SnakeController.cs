using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : ModuleController
{
    [SerializeField] private InputControl _inputControl;
    public InputControl InputControl => _inputControl;

    [SerializeField] private PlayerData _data;
    public PlayerData Data => _data;

    [SerializeField] private bool _isHead;
    public bool IsHead => _isHead;

    public SnakeController Head { get; private set; }
    public SnakeController Parent { get; private set; }
    public SnakeController Child { get; private set; }
    
    public int Index { get; private set; }
    public int PartsCnt { get; private set; }

    private List<Vector3> _positionHistory;
    public List<Vector3> PositionHistory => _positionHistory;

    private void Awake()
    {
        if (_isHead)
        {
            _positionHistory = new List<Vector3>();
            Index = 0;
            PartsCnt = 0;
        }
        
        AddModule(new SnakeMovementModule(this));
        AddModule(new SnakeRotateModule(this));
    }

    public override void Update()
    {
        base.Update();
        if (IsHead && Keyboard.current.wKey.wasPressedThisFrame)
        {
            GrowUp();
        }
    }

    public void GrowUp()
    {
        if (_isHead)
        {
            PartsCnt++;
        }
        
        if (Child is null)
        {
            Child = PoolManager.Instance.Pop("SnakePart") as SnakeController;
            Child.Head = _isHead ? this : Head;
            Child.Parent = this;
            Child.Index = Index + 1;
            return;
        }
        
        Child.GrowUp();
    }

    public override void Init()
    {
        base.Init();
        Child = null;
    }
}