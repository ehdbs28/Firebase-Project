using System;
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

    public void Play(Action callBack = null)
    {
        StartCoroutine(PlayRoutine(callBack));
    }

    private IEnumerator PlayRoutine(Action callBack = null)
    {
        var duration = _particleSystem.main.duration;
        _particleSystem.Play();
        yield return new WaitForSeconds(duration);
        callBack?.Invoke();
        _particleSystem.Stop();
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
    }
}