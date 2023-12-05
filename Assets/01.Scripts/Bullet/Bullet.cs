using System;
using UnityEngine;

public class Bullet : PoolableMono
{
    private Rigidbody2D _rigidbody;
    private TrailRenderer _trailRenderer;

    private Vector3 _dir;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (OutScreenBound())
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        var particle = PoolManager.Instance.Pop("BulletDestroyEffect") as PoolableParticle;
        var angle = Mathf.Atan2(-_dir.y, -_dir.x) * Mathf.Rad2Deg - 90f;
        
        particle.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        particle.Play();
        
        PoolManager.Instance.Push(this);
    }

    public void Setting(Vector3 dir, float speed)
    {
        _dir = dir;
        var angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _rigidbody.velocity = _dir * speed;
        _trailRenderer.Clear();
        _trailRenderer.enabled = true;
    }

    private bool OutScreenBound()
    {
        var worldHeight = GameManager.Instance.MainCam.orthographicSize * 2f;
        var worldWidth = worldHeight / Screen.height * Screen.width;
        var pos = transform.position;
        return pos.x < -worldWidth / 2f || pos.x > worldWidth / 2f ||
               pos.y < -worldHeight / 2f || pos.y > worldHeight / 2f;
    }

    public override void Init()
    {
        _trailRenderer.enabled = false;
    }
}