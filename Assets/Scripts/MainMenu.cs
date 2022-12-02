
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase.Messaging;
using System;

public class MainMenu : MonoBehaviour
{
    public GameObject UI_Start;
    public GameObject UI_Choose;
    public GameObject UI_NoInternet;
    public GameObject UI_Black;
    public GameObject Video;

    public GameObject Text_Cong;
    public GameObject Text_Up;

    public GameObject Text_TimerOn;
    public Text TextTimer;
    
    int incTimer = 0;

    
    float timeup = 5f;

    DataBase db;
    FadeScene FadeScene;

    public void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 1;
        FadeScene = GetComponent<FadeScene>();
        FadeScene.startscene();

        if (Counter.BeginStart)
        {
            UI_Choose.SetActive(true);
            UI_Choose.GetComponent<FadeRL>().OffFide();
            UI_Start.SetActive(false);
        } 
        else Counter.BeginStart = true;

        db = GetComponent<DataBase>();
        CheckInternet();
        Invoke("UpText", timeup);
    }

    void UpText()
    {
        Text_Cong.SetActive(false);
        Text_Up.SetActive(true);
    }

    private void FixedUpdate()
    {
        string date = db.TimerTur();
        if (date != null)
        {
            if (DateTime.Parse(date) < DateTime.Now)
            {
                TextTimer.text = "00:00:00";
                Counter.In_Timer = false;
            }
            else
            {
                var DateT = DateTime.Parse(date) - DateTime.Now;
                TextTimer.text = Mathf.Floor((float)DateT.TotalHours).ToString("00") + ":" + DateT.Minutes.ToString("00")
                    + ":" + DateT.Seconds.ToString("00");
                Counter.In_Timer = true;
            }
            Text_TimerOn.SetActive(!Counter.In_Timer);

        }



    }
    private void Update()
    {
        incTimer++;
       
        if (incTimer == 61)
        {
            incTimer = 0;
            CheckInternet();
        }
    }


    public void OnButtonMod3() 
    {
        if (!Counter.In_Internet) OnNext();
       if (Counter.In_Timer) 
            LoadSceneMod(3); 
    }
    public void OnButtonMod5() 
    { 
        if (!Counter.In_Internet) OnNext(); 
        if (Counter.In_Timer) 
            LoadSceneMod(5); 
    }

    void LoadSceneMod(int i)
    {
        Counter.MaxCountBall = i;
        FadeScene.loadscene("Game");
    }

    public void OnTable()
    {
        FadeScene.loadscene("MenuTable");
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnNext()
    {
        
        CheckInternet();
        UI_Start.GetComponent<FadeRL>().OnFadeL(false, 0 , Screen.width * 1.2f);
        Video.SetActive(false);
        if (!Counter.In_Internet)
        {
            UI_NoInternet.SetActive(true);
            UI_Choose.SetActive(false);
        }
        else
        {
            UI_NoInternet.SetActive(false);
            UI_Choose.SetActive(true);
            UI_Choose.GetComponent<FadeRL>().OnFadeL(true, -Screen.width * 1.2f, 0);
        }
    }

    void CheckInternet()
    {
        StartCoroutine(TestConnection());
    }

    void CheckTimer()
    {
        db.CheckTimer();
    }

    IEnumerator TestConnection()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://google.com");
        yield return request.SendWebRequest();
        Counter.In_Internet = !request.isNetworkError;
    }

    public void Message()
    {
        Firebase.Messaging.FirebaseMessage message = new Firebase.Messaging.FirebaseMessage();

    }
}
