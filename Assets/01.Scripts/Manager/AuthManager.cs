using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class AuthManager
{
    public static AuthManager Instance = null;
    
    private FirebaseAuth _auth;

    public AuthManager()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                _auth = FirebaseAuth.DefaultInstance;
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    public async Task<bool> SignUp(string email, string password)
    {
        var success = true;
        await _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled");
                success = false;
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError($"CreateUserWithEmailAndPasswordAsync encountered an error: {task.Exception}");
                success = false;
                return;
            }

            var newUser = task.Result.User;
            Debug.Log($"Firebase user created successfully: {newUser.DisplayName}({newUser.UserId})");
        });
        return success;
    }

    public bool SignIn(string email, string password)
    {
        var success = true;
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled");
                success = false;
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError($"SignInWithEmailAndPasswordAsync an error: {task.Exception}");
                success = false;
                return;
            }

            var newUser = task.Result.User;
            Debug.Log($"Firebase user sign in successfully: {newUser.DisplayName}({newUser.UserId})");
        });
        return success;
    }
}
