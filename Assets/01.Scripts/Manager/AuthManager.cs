using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public struct UserData
{
    public string name;
    public int score;
}

public class AuthManager
{
    public static AuthManager Instance = null;

    private FirebaseAuth _auth;
    private DatabaseReference _dbReference;
    private FirebaseUser _user;
    public FirebaseUser User => _user;

    private int _dayCount;
    public int DayCount => _dayCount;

    private Dictionary<string, UserData> _users = new Dictionary<string, UserData>();
    public Dictionary<string, UserData> Users => _users;

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
            await SaveLastLogin();
            await SaveDayCount(1);
            await LoadUsers();
            // await SetBirthday();

            Debug.Log($"Firebase user created successfully: {_user.DisplayName}({_user.UserId})");
        });
        return success;
    }

    public async Task<bool> SignIn(string email, string password)
    {
        var success = true;
        await _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(async task =>
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
            // await LoadDayCount();
            // await LoadLastLogin();
            await SaveLastLogin();
            await LoadUsers();
            // await SetBirthday();
            Debug.Log($"Firebase user sign in successfully: {_user.DisplayName}({_user.UserId})");
        });
        return success;
    }

    private async Task LoadUsers()
    {
        _users.Clear();
        
        var task = _dbReference.Child("users").GetValueAsync();
        await task;

        foreach (var child in task.Result.Children)
        {
            var name = child.Child("UserName").Value.ToString();
            var score = child.Child("Score").Value;
            Debug.Log(name);
            _users.Add(child.Value.ToString(), new UserData{ name = name, score = (int)score });
        }
    }

    private async Task SaveUserName(string nickname)
    {
        var task = _dbReference.Child("users").Child(_user.UserId).Child("UserName").SetValueAsync(nickname);
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

    private async Task SaveLastLogin()
    {
        var curTime = DateTime.Now.ToString("yyyyMMdd");
        var task = _dbReference.Child("users").Child(_user.UserId).Child("LastLogin").SetValueAsync(curTime);
        await task;

        if (task.Exception != null)
        {
            Debug.LogWarning($"Save LastLogin task failed with {task.Exception}");
        }
        else
        {
            Debug.Log("Save Completed");
        }
    }

    private async Task SaveDayCount(int dayCount)
    {
        var task = _dbReference.Child("users").Child(_user.UserId).Child("DayCount").SetValueAsync(dayCount);
        await task;
    }

    private async Task SetBirthday()
    {
        var task = _dbReference.Child("Birthday").GetValueAsync();
        await task;
        
        if (task.Exception != null)
        {
            Debug.LogWarning($"Set Birthday task failed with {task.Exception}");
            return;
        }
    
        var snapshot = task.Result;
        var curDate = DateTime.Today.Month.ToString("D2") + DateTime.Today.Day.ToString("D2");
    
        if(curDate == snapshot.Value.ToString())
        {
            var particle = PoolManager.Instance.Pop("BirthdayParticle") as PoolableParticle;
            particle.Play();
        }
    }

    private async Task LoadDayCount()
    {
        var task = _dbReference.Child("users").Child(_user.UserId).Child("DayCount").GetValueAsync();
        await task;

        if (task.Exception != null)
        {
            return;
        }
        
        var snapshot = task.Result;
        _dayCount = (int)snapshot.Value;
    }

    private async Task LoadLastLogin()
    {
        var curTime = DateTime.Now.ToString("yyyyMMdd");
        var task = _dbReference.Child("users").Child(_user.UserId).Child("LastLogin").GetValueAsync();
        await task;

        if (task.Exception != null)
        {
            return;
        }

        var snapshot = task.Result;
        var diff = int.Parse(curTime) - int.Parse(snapshot.Value.ToString());

        if (diff == 1)
        {
            _dayCount++;
            await SaveDayCount(_dayCount + 1);
        }
        else
        {
            await SaveDayCount(1);
        }
    }

    public void SetFriend(string aId, string bId)
    {
        if (aId == bId)
        {
            return;
        }
        
        _dbReference.Child("users").Child(aId).Child("Friend").SetValueAsync(bId);
        _dbReference.Child("users").Child(bId).Child("Friend").SetValueAsync(bId);
    }

    public void SetScore(int score)
    {
        _dbReference.Child("users").Child(_user.UserId).Child("Score").SetValueAsync(score);
    }
}
