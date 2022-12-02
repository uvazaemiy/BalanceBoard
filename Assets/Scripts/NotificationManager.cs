using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Messaging;
using Firebase.Installations;

public class NotificationManager : MonoBehaviour
{
    public void Start()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReseived;
    }

    public void OnTokenReceived (object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    { 
        UnityEngine.Debug.Log("Token " + token.Token);
    }

    public void OnMessageReseived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        UnityEngine.Debug.Log("Message " + e.Message.From);
    }

    
  
}
