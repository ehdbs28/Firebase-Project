using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class AuthManager
{
    public static AuthManager Instance = null;
    
    private FirebaseAuth _auth;
    private DatabaseReference _dbReference;
    private FirebaseUser _user;

    public AuthManager()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                _auth = FirebaseAuth.DefaultInstance;
                _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    public async Task<bool> SignUp(string email, string password, string nickname)
    {
        var success = true;
        await _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(async task =>
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

            _user = task.Result.User;
            
            var user = _auth.CurrentUser;
            var profile = new UserProfile
            {
                DisplayName = nickname
            };
            var profileTask = user.UpdateUserProfileAsync(profile);
            await profileTask;
            await SaveUserName(nickname);
            
            Debug.Log($"Firebase user created successfully: {_user.DisplayName}({_user.UserId})");
        });
        return success;
    }

    public async Task<bool> SignIn(string email, string password)
    {
        var success = true;
        await _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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

            _user = task.Result.User;
            Debug.Log($"Firebase user sign in successfully: {_user.DisplayName}({_user.UserId})");
        });
        return success;
    }

    private async Task SaveUserName(string nickname)
    {
        var task =  _dbReference.Child("users").Child(_user.UserId).Child("UserName").SetValueAsync(nickname);
        await task;

        if (task.Exception != null)
        {
            Debug.LogWarning($"Load Task failed with {task.Exception}");
        }
        else
        {
            Debug.Log("Same Completed");
        }
    }

    private async void LoadUserName()
    {
        var task = _dbReference.Child("users").Child(_user.UserId).Child("UserName").GetValueAsync();
        await task;

        if (task.Exception != null)
        {
            Debug.LogWarning($"Load Task failed with {task.Exception}");
        }
        else
        {
            var snapshot = task.Result;
            Debug.Log("Load Complete");
        }
    }
}
