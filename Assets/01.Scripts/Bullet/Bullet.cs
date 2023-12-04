using System;
using UnityEngine;

public class Bullet : PoolableMono
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Setting(Vector3 dir, float speed)
    {
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _rigidbody.velocity = dir * speed;
    }

    public override void Init()
    {
    }
}