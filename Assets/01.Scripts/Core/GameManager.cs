using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private PoolingList _poolingList;

    private Camera _mainCam;
    public Camera MainCam => _mainCam;

    private SnakeController _snake;
    public SnakeController Snake => _snake;

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

        _snake = FindObjectOfType<SnakeController>();
        
        DontDestroyOnLoad(gameObject);
    }

    private void CreateManager()
    {
        AuthManager.Instance = new AuthManager();
        PoolManager.Instance = new PoolManager();
        ScoreManager.Instance = GetComponent<ScoreManager>();
        UIManager.Instance = new UIManager();
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