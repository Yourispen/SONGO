using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using System;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine.Events;

public class Firebase_Scripts : MonoBehaviour
{
    public UnityEvent onFirebaseInitialized;
    

    private void Awake()
    {
        StartCoroutine(CheckAndFixDependenciesCoroutine());
    }


    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
       {
           FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
       });
    }
    
    private IEnumerator CheckAndFixDependenciesCoroutine()
    {
        var checkDependenciesTask = Firebase.FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => checkDependenciesTask.IsCompleted);

        var dependencyStatus = checkDependenciesTask.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
            Debug.Log($"Firebase: {dependencyStatus} :)");
            onFirebaseInitialized.Invoke();
        }
        else
        {
            Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
    }

}
