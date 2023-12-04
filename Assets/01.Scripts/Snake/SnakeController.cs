using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : ModuleController, IDamageable
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

    private bool _isDetached;

    #region Properties

    public InputControl InputControl => _inputControl;
    public PlayerData Data => _data;
    public bool IsHead => _isHead;
    public SnakeController Head => _head;
    public SnakeController Parent => _parent;
    public int Index => _index;
    public int PartsCnt => _partsCnt;
    public List<Vector3> PositionHistory => _positionHistory;

    #endregion

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
        AddModule(new SnakeHealthModule(this));
    }

    public override void Update()
    {
        if (_isDetached || (_parent is not null && _parent._isDetached))
        {
            return;
        }

        base.Update();
    }

    public override void FixedUpdate()
    {
        if (_isDetached || (_parent is not null && _parent._isDetached))
        {
            return;
        }
        base.FixedUpdate();
    }

    public void Setting(SnakeController head, SnakeController parent, int index)
    {
        _child = null;
        _isDetached = false;
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

    public void Detach(int detachPointIndex)
    {
        if (_isDetached)
        {
            return;
        }

        if (_parent)
        {
            _parent._child = null;
        }
        
        _isDetached = true;
        _child?.Detach(detachPointIndex);
        StartCoroutine(DetachRoutine((_index - detachPointIndex) * 0.1f));
    }

    private IEnumerator DetachRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isHead || _isDetached)
        {
            return;
        }
        
        if (other.CompareTag("Item"))
        {
            GrowUp();
        }
    }

    public void OnDamage(float damage)
    {
        GetModule<SnakeHealthModule>().OnDamage(damage);
    }
}