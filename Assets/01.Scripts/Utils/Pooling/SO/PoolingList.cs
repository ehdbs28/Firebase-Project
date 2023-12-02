using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolingItem
{
    public PoolableMono prefab;
    public int cnt;
}

[CreateAssetMenu(menuName = "SO/PoolingList")]
public class PoolingList : ScriptableObject
{
    public List<PoolingItem> poolingItems = new List<PoolingItem>();
}