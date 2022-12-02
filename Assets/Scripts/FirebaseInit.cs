using System;
using Firebase;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseInit : MonoBehaviour
{
    public UnityEvent OnInitialized = new UnityEvent();
    public InitializationFailedEvent OnInitializationFailed = new InitializationFailedEvent();

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                OnInitializationFailed.Invoke(task.Exception);
                Debug.Log($"Firebased initialization failed!");
            }
            else
            {
                OnInitialized.Invoke();
                Debug.Log("Firebased initialized!");
            }
        });
    }

    [Serializable]
    public class InitializationFailedEvent : UnityEvent<Exception>
    {
    }
}