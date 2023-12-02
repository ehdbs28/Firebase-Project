using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private PoolingList _poolingList;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("GameManager instance is already exist");
            return;
        }
        
        Instance = this;
        
        CreateManager();
        CreatePool();
        
        DontDestroyOnLoad(gameObject);
    }

    private void CreateManager()
    {
        AuthManager.Instance = new AuthManager();
        PoolManager.Instance = new PoolManager();
    }

    private void CreatePool()
    {
        foreach (var poolingItem in _poolingList.poolingItems)
        {
            PoolManager.Instance.CreatePool(poolingItem.prefab, transform, poolingItem.cnt);
        }
    }
}