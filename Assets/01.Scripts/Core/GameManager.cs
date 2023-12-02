using System;
using Firebase.Auth;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("GameManager instance is already exist");
            return;
        }
        
        Instance = this;
        
        CreateManager();
        
        DontDestroyOnLoad(gameObject);
    }

    private void CreateManager()
    {
        AuthManager.Instance = GetComponent<AuthManager>();
    }
}