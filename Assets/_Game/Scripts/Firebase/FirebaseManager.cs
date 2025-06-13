using System;
using Firebase;
using Firebase.Auth;
using UnityEngine;
using Firebase.Extensions;

public class FirebaseManager
{
    public bool IsInitialized { get; private set; }
    public FirebaseUser CurrentUser => FirebaseAuth.DefaultInstance.CurrentUser;
    public bool IsAuthenticated => CurrentUser != null;

    FirebaseAuth _auth;

    public void Initialize()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                DependencyStatus dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseApp app = FirebaseApp.DefaultInstance;
                    DebugUtils.Log("Firebase pronto");
                    _auth = FirebaseAuth.DefaultInstance;
                    IsInitialized = true;
                }
                else
                {
                    DebugUtils.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            }
        );
    }

    public void Login (string email, string password, Action<bool> onComplete)
    {
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    DebugUtils.LogWarning("Login failed: " + task.Exception?.Flatten().Message);
                    onComplete(false);
                    return;
                }

                onComplete(true);
            }
        );
    }

    public void Register (string email, string password, Action<bool> onComplete)
    {
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    DebugUtils.LogWarning("Registration failed: " + task.Exception?.Flatten().Message);
                    onComplete(false);
                    return;
                }

                onComplete(true);
            }
        );
    }
}