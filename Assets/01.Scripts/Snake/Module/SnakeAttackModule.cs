using System.Linq;
using UnityEngine;

public class SnakeAttackModule : BaseModule<SnakeController>
{
    private float _attackDelay;
    private float _lastAttackTime;

    private EnemyController _target;

    private Vector3 _attackDir;
    public Vector3 AttackDir => _attackDir;

    private Transform _firePos;

    public SnakeAttackModule(SnakeController controller) : base(controller)
    {
        _attackDelay = Random.Range(Controller.Data.attackDelayMin, Controller.Data.attackDelayMax);
        _lastAttackTime = Time.time;
        _firePos = controller.transform.Find("Visual/Tank/FirePos");
    }

    public override void UpdateModule()
    {
        CalcAttackDir();
        if (_target && Time.time >= _lastAttackTime + _attackDelay)
        {
            Attack();   
        }
    }

    public override void FixedUpdateModule()
    {
    }

    private void Attack()
    {
        _lastAttackTime = Time.time;

        var bullet = PoolManager.Instance.Pop("Bullet") as Bullet;
        
        bullet.transform.position = _firePos.position;
        bullet.Setting(_attackDir, Controller.Data.bulletSpeed);
    }

    private void CalcAttackDir()
    {
        var enemies = StageManager.Instance.Builder.Enemies;

        if (enemies.Count > 0)
        {
            _target = enemies.OrderBy(enemy => Vector3.Distance(Controller.transform.position, enemy.transform.position)).First();
            _attackDir = (_target.transform.position - Controller.transform.position).normalized;
        }
        else
        {
            _target = null;
        }
    }
}
