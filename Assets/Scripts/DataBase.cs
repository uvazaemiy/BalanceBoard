using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using System;

public class DataBase : MonoBehaviour
{
    
    public GameObject Table;
    public GameObject Cell_Name;
    public GameObject Cell_Place;

    private Coroutine CRLoadData;
    private Coroutine CRTimer;

    private DatabaseReference dbRef;

    long timeTournament;
    string Date;

    public void SaveData(string name, string mail, int balls)
    {
        float time = 0;
        if (balls == 3) time = Counter.BestTime3;
        if (balls == 5) time = Counter.BestTime5;

        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        User user = new User(name, mail, balls,time);
        string json = JsonUtility.ToJson(user);
        dbRef.Child("balls").Child(balls.ToString()).Child(name).SetRawJsonValueAsync(json);   
    }

    private IEnumerator IELoadData(int balls)
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        var users = dbRef.Child("balls").Child(balls.ToString()).OrderByChild("time").GetValueAsync();

        yield return new WaitUntil(predicate: () => users.IsCompleted);

        if (users.Exception != null)
        {
            Debug.LogError(users.Exception);
        }
        else if (users.Result == null)
        {
            Debug.Log("null");
        }
        else
        {
            foreach (Transform Child in Table.transform)
            {
                Destroy(Child.gameObject);
            }
            Instantiate(Cell_Name, Table.transform);
            Instantiate(Cell_Place, Table.transform);
            DataSnapshot snapshot = users.Result;
            int place = 1;
            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                GameObject TextTable = Instantiate(Cell_Name, Table.transform);
                TextTable.transform.GetChild(0).GetComponent<Text>().text = childSnapshot.Child("name").Value.ToString();
                TextTable = Instantiate(Cell_Place, Table.transform);
                long time = (long)childSnapshot.Child("time").GetValue(false);
                string timedol = (time % 100).ToString();
                string timesec = (time / 100 % 60).ToString();
                string timemin = (time / 100 / 60).ToString();
                

                TextTable.transform.GetChild(0).GetComponent<Text>().text = "       " + place + "              " 
                    + zero(timemin) + ":" + zero(timesec) + ":" + zero(timedol);
                place++;
            }
        }
    }
    
    string zero(string t)
    {
        if (t.Length == 1) return "0" + t; else return t;
    }

    public void LoadData(int balls) 
    {
        if (CRLoadData != null)
            StopCoroutine(CRLoadData);

        CRLoadData = StartCoroutine(IELoadData(balls)); 
    }

    public string TimerTur()
    {
        if (!Counter.In_Internet) timeTournament = 0;
        if (CRTimer != null)
            StopCoroutine(CRTimer);

        CRTimer = StartCoroutine(IETimer());
        // string timeMin = (timeTournament % 3600 / 60).ToString();
        //string timeHour = (timeTournament / 3600).ToString();
        

        //string result = zero(timeHour) + ":" + zero(timeMin);

        return Date; 
    }


    private IEnumerator IETimer()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        //var users = dbRef.Child("admin").Child("timer").GetValueAsync();
        var users = dbRef.Child("admin").Child("data").GetValueAsync();

        yield return new WaitUntil(predicate: () => users.IsCompleted);

        if (users.Exception != null)
        {
            Debug.LogError(users.Exception);
        }
        else if (users.Result == null)
        {
            Debug.Log("null");
        }
        else
        {
            DataSnapshot snapshot = users.Result;
            Date = (string)snapshot.GetValue(true);

            //timeTournament = (long) snapshot.GetValue(false);
        }
    }


    public void CheckTimer()
    {
        //if (TimerTur() != "00:00") Counter.In_Timer = true; else Counter.In_Timer = false;
    }

    
}
