using System;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance = null;
    
    private FirebaseApp _app;
    private FirebaseAuth _auth;

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    public void SignUp(string email, string password)
    {
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError($"CreateUserWithEmailAndPasswordAsync encountered an error: {task.Exception}");
                return;
            }

            var newUser = task.Result.User;
            Debug.Log($"Firebase user created successfully: {newUser.DisplayName}({newUser.UserId})");
        });
    }

    public void SignIn(string email, string password)
    {
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError($"SignInWithEmailAndPasswordAsync an error: {task.Exception}");
                return;
            }

            var newUser = task.Result.User;
            Debug.Log($"Firebase user sign in successfully: {newUser.DisplayName}({newUser.UserId})");
        });
    }
}
