using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private PoolingList _poolingList;

    private Camera _mainCam;
    public Camera MainCam => _mainCam;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("GameManager instance is already exist");
            return;
        }
        
        Instance = this;
        _mainCam = Camera.main;
        
        CreateManager();
        CreatePool();
        
        DontDestroyOnLoad(gameObject);
    }

    private void CreateManager()
    {
        AuthManager.Instance = new AuthManager();
        PoolManager.Instance = new PoolManager();
        StageManager.Instance = GetComponent<StageManager>();
    }

    private void CreatePool()
    {
        foreach (var poolingItem in _poolingList.poolingItems)
        {
            PoolManager.Instance.CreatePool(poolingItem.prefab, transform, poolingItem.cnt);
        }
    }
}