using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : ModuleController, IDamageable
{
    [SerializeField] private InputControl _inputControl;
    [SerializeField] private SnakeData _data;
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
    public SnakeData Data => _data;
    public bool IsHead => _isHead;
    public bool IsDetached => _isDetached;
    public SnakeController Head => _head;
    public SnakeController Parent => _parent;
    public int Index => _index;
    public int PartsCnt => _partsCnt;
    public List<Vector3> PositionHistory => _positionHistory;

    #endregion
    
    public void OnEnable()
    {
        RemoveAllModule();
        
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

        if (!_isHead)
        {
            AddModule(new SnakeAttackModule(this));
        }
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

    public List<SnakeController> GetParts()
    {
        if (!_isHead)
        {
            return null;
        }

        var list = new List<SnakeController>();
        var head = this;

        while (head != null)
        {
            list.Add(head);
            head = head._child;
        }

        return list;
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

        if (_isHead)
        {
            Debug.Log("GameOver");
            StageManager.Instance.DisableStage();
        }
        else
        {
            StartCoroutine(DetachRoutine((_index - detachPointIndex) * 0.1f));
        }
    }

    private IEnumerator DetachRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        var destroyEffect = PoolManager.Instance.Pop("DestroyEffect") as PoolableParticle;
        destroyEffect.SetPositionAndRotation(transform.position);
        destroyEffect.Play();
        
        PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDetached)
        {
            return;
        }
        
        if (_isHead && other.CompareTag("Item"))
        {
            GrowUp();
            ScoreManager.Instance.ScoreUp();
            StageManager.Instance.ItemBuilder.SpawnItem();
        }

        if (other.CompareTag("Enemy"))
        {
            OnDamage();
        }
    }

    public void OnDamage()
    {
        GetModule<SnakeHealthModule>().OnDamage();
    }
}