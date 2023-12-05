using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuildArea : MonoBehaviour
{
    [SerializeField] private float _width;
    [SerializeField] private float _height;

    public Vector2 GetAreaPosition()
    {
        var pos = transform.position;
        var x = Random.Range(pos.x - _width / 2f, pos.x + _width / 2f);
        var y = Random.Range(pos.y - _height / 2f, pos.y + _height / 2f);
        return new Vector2(x, y);
    }
    
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, new Vector3(_width, _height));
    }

#endif
}