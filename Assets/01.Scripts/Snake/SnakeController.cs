using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : ModuleController
{
    [SerializeField] private InputControl _inputControl;
    [SerializeField] private PlayerData _data;
    [SerializeField] private bool _isHead;

    private SnakeController _head;
    private SnakeController _parent;
    private SnakeController _child;
    
    private List<Vector3> _positionHistory;
    private int _index;
    private int _partsCnt;

    private bool _isDead;

    public InputControl InputControl => _inputControl;
    public PlayerData Data => _data;
    public bool IsHead => _isHead;
    public SnakeController Head => _head;
    public SnakeController Parent => _parent;
    public int Index => _index;
    public int PartsCnt => _partsCnt;
    public List<Vector3> PositionHistory => _positionHistory;

    private void Awake()
    {
        if (_isHead)
        {
            _positionHistory = new List<Vector3>();
            _index = 0;
            _partsCnt = 0;
            _head = this;
        }
        
        AddModule(new SnakeMovementModule(this));
        AddModule(new SnakeRotateModule(this));
    }

    public void Setting(SnakeController head, SnakeController parent, int index)
    {
        _child = null;
        _head = head;
        _parent = parent;
        _index = index;
    }

    private void GrowUp()
    {
        if (_isHead)
        {
            _partsCnt++;
        }
        
        if (_child is null)
        {
            _child = PoolManager.Instance.Pop("SnakePart") as SnakeController;
            _child.Setting(_head, this, _index + 1);
            return;
        }
        
        _child.GrowUp();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isHead || _isDead)
        {
            return;
        }
        
        if (other.CompareTag("Item"))
        {
            GrowUp();
        }
    }
}