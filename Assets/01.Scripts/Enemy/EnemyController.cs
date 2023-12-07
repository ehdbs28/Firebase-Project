using System;
using System.Linq;
using UnityEngine;

public class EnemyController : ModuleController, IDamageable
{
    [SerializeField] private EnemyData _data;
    
    private SnakeController _target;

    public SnakeController Target => _target;
    public EnemyData Data => _data;
    
    public void OnEnable()
    {
        _target = null;
        RemoveAllModule();
        AddModule(new EnemyMovementModule(this));
        AddModule(new EnemyRotateModule(this));
        AddModule(new EnemyHealthModule(this));
    }

    public override void Update()
    {
        if (_target is null || _target.IsDetached)
        {
            TargetSetting();
        }
        base.Update();
    }

    private void TargetSetting()
    {
        var snakeParts = GameManager.Instance.Snake.GetParts();
        _target = snakeParts.OrderBy(part => Vector3.Distance(transform.position, part.transform.position)).First();
    }
    
    public void OnDamage()
    {
        GetModule<EnemyHealthModule>().OnDamage();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            OnDamage();
            other.GetComponent<Bullet>().DestroyObject();
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_target != null)
        {
            Gizmos.DrawLine(transform.position, _target.transform.position);
        }
    }

#endif
}
