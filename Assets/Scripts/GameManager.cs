using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Ball;
    public GameObject BallParent;

    public GameObject MenuPause;
    public GameObject MenuWin;
    public GameObject MenuOverboard;
    public GameObject ButtonPause;
    public GameObject MenuRegistration;

    public Text TimeText; 
    public Text TimeWinText;
    public Text BestTimeText;
    public InputField InputName;
    public InputField InputMail;

    float dolTime;
    float secTime;
    float minTime;

    DataBase db;
    FadeScene FadeScene;

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Counter.Overboard = false;
        Counter.CountBall = Counter.MaxCountBall;
        Time.timeScale = 1;
        for (int i = 0; i < Counter.MaxCountBall; i++)
        {
            GameObject GOball = Instantiate(Ball, BallParent.transform);
            GOball.transform.position = GOball.transform.position + new Vector3(Random.Range(-4f, 4f), 0, Random.Range(-4f, 4f));
        }
        db = GetComponent<DataBase>();
        FadeScene = GetComponent<FadeScene>();
        FadeScene.startscene();
    }

    void FixedUpdate()
    {
        if (Counter.CountBall == 0)
        {
            Counter.CountBall--;
            OnWin();
        }
        ScoreCounter();
        if (Counter.Overboard) OnOverboard();
    }



    void ScoreCounter()
    {
        dolTime += Time.deltaTime * 100 ;
        if (dolTime >= 100)
        {
            dolTime -= 100;
            secTime++;
        }
        if (secTime >= 60)
        {
            secTime -= 60;
            minTime++;
        }
        TimeText.text = zero(minTime.ToString()) + ":" + zero(secTime.ToString()) + ":" + zero(((int)dolTime).ToString());
    }

    string zero(string t)
    {
        if (t.Length == 1) return "0" + t; else return t;
    }

    void OnWin()
    {
        TimeText.gameObject.SetActive(false);
        Time.timeScale = 0;
        
        ButtonPause.GetComponent<EnableUI>()._OnDisable();

        Counter.TimeCount = (minTime * 60 + secTime) * 100 + dolTime;

        if (Counter.MaxCountBall == 3 && (Counter.TimeCount < Counter.BestTime3 || Counter.BestTime3 == 0))
        {
            Counter.BestTime3 = Counter.TimeCount;
            BestTimeText.text = zero(((int)Counter.BestTime3 / 100 / 60).ToString()) 
                + ":" + zero(((int)Counter.BestTime3 / 100 % 60).ToString())
                + ":" + zero(((int)Counter.BestTime3 % 100).ToString());
        } 
        else 
        if (Counter.MaxCountBall == 5 && (Counter.TimeCount < Counter.BestTime5 || Counter.BestTime5 == 0))
        {
            Counter.BestTime5 = Counter.TimeCount;
            BestTimeText.text = zero(((int)Counter.BestTime5 / 100 / 60).ToString())
                + ":" + zero(((int)Counter.BestTime5 / 100 % 60).ToString())
                + ":" + zero(((int)Counter.BestTime5 % 100).ToString());
        }

        TimeWinText.text = TimeText.text;
        if (!Counter.BeginWin)
        {
            MenuRegistration.SetActive(true);
        }
        else
        {
            MenuWin.SetActive(true);
            db.SaveData(Counter.UserName, Counter.UserMail, Counter.MaxCountBall);
        }
    }

    public void OnPause()
    {
        Time.timeScale = 0;
        MenuPause.SetActive(true);
        ButtonPause.GetComponent<EnableUI>()._OnDisable();
    }

    public void OnContinue()
    {
        Time.timeScale = 1;
        MenuPause.GetComponent<EnableUI>()._OnDisable();
        ButtonPause.SetActive(true);
    }

    public void OnMainMenu()
    {
        FadeScene.loadscene("MainMenu");
    }

    public void OnSaveRegistration()
    {
        Counter.UserName = (string) InputName.text;
        Counter.UserMail = (string) InputMail.text;
        MenuRegistration.GetComponent<FadeRL>().OnFadeL(false, 0, Screen.width * 1.2f);
        //MenuRegistration.GetComponent<EnableUI>()._OnDisable();
        MenuWin.SetActive(true);
        MenuWin.GetComponent<FadeRL>().OnFadeL(true, -Screen.width * 1.2f, 0);
        if ((Counter.UserName != null) && (Counter.UserMail != null))
        {
            Counter.BeginWin = true;
            db.SaveData(Counter.UserName, Counter.UserMail, Counter.MaxCountBall);
        }
    }

    public void OnOverboard()
    {
        Time.timeScale = 0;
        MenuOverboard.SetActive(true);
        ButtonPause.GetComponent<EnableUI>()._OnDisable();
        TimeText.gameObject.SetActive(false);
    }

    public void OnRestart()
    {
        FadeScene.loadscene("Game");
    }
}
