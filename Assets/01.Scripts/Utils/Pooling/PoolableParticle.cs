using System.Collections;
using UnityEngine;

public class PoolableParticle : PoolableMono
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void SetPositionAndRotation(Vector3 position = default, Quaternion rotation = default)
    {
        _particleSystem.transform.SetPositionAndRotation(position, rotation);
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        var duration = _particleSystem.main.duration;
        _particleSystem.Play();
        yield return new WaitForSeconds(duration);
        _particleSystem.Stop();
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
    }
}