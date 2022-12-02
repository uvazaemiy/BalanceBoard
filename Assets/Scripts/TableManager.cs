using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    
    int balls;

    DataBase db;
    FadeScene FadeScene;

    public Text TextChoose;

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        db = GetComponent<DataBase>();
        OnChooseBalls();
        FadeScene = GetComponent<FadeScene>();
        FadeScene.startscene();
    }

    public void OnMainMenu()
    {
        FadeScene.loadscene("MainMenu");
    }

    public void OnChooseBalls()
    {
        if (balls == 5) 
        { 
            balls = 3;
            TextChoose.text = "<color=blue> 3 уровень </color><color=grey>/ 5 </color>";
        } 
        else 
        {
            balls = 5;
            TextChoose.text = "<color=grey> 3 / </color><color=blue>5 уровень </color>";
        }
        db.LoadData(balls); 
    }
   
}
