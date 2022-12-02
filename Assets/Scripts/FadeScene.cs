using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour // Нужно совместить с FadeBlack и EnableUI, все трое делают одно и тоже
{
    Color _color;
    float Speed = 4f;
    string scene;

    public GameObject UI_FadeScene;
    public GameObject UI;

    IEnumerator FadeBlack()
    {
        UI_FadeScene.SetActive(true);
        _color = UI_FadeScene.GetComponent<Image>().color;
        
        for (float i = 0; i < 1f; i += Speed * 0.015f)
        {
            _color.a = i;
            UI_FadeScene.GetComponent<Image>().color = _color;
            yield return null;
        }
        UI.SetActive(false);
        SceneManager.LoadScene(scene);
    }

    public void loadscene(string sc)
    {
        scene = sc;
        StartCoroutine(FadeBlack());
    }

    IEnumerator FadeWhite()
    {
        UI_FadeScene.SetActive(true);
        _color = UI_FadeScene.GetComponent<Image>().color;
        for (float i = 1; i > 0; i -= Speed * 0.015f)
        {
            _color.a = i;
            UI_FadeScene.GetComponent<Image>().color = _color;
            yield return null;
        }
        UI_FadeScene.SetActive(false);
    }

    public void startscene()
    {
        StartCoroutine(FadeWhite());
    }
}
