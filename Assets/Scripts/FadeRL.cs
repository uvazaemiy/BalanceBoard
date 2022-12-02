using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FadeRL : MonoBehaviour
{

    float Speed = 3000 * 20;
    RectTransform rec;
    Color _color;
    float BeginAlpha;
    float SpeedAlph = 0.8f * 15;

    public GameObject UI_Fade;


    private void Awake()
    {
        _color = UI_Fade.GetComponent<Image>().color;
        BeginAlpha = _color.a;
    }


    public void OnFadeL(bool noActive, float A, float B)
    {
        StartCoroutine(IEFadeL(noActive,A,B));
    }

    IEnumerator IEFadeL(bool noActive, float A, float B)
    {
        rec = GetComponent<RectTransform>();

        if (noActive) _OnDisable(); else _OnEnable();
        for (float i = A; i <= B; i += Speed * (Screen.width / Speed / 10))
        {
            SetLeft(-i);
            SetRight(i);
            yield return null;
        }
        gameObject.SetActive(noActive);
    }

    public void SetLeft(float left)
    {
        rec.offsetMin = new Vector2(left, rec.offsetMin.y);
    }

    public void SetRight(float right)
    {
        rec.offsetMax = new Vector2(-right, rec.offsetMax.y);
    }

    public void SetTop(float top)
    {
        rec.offsetMax = new Vector2(rec.offsetMax.x, -top);
    }

    public void SetBottom(float bottom)
    {
        rec.offsetMin = new Vector2(rec.offsetMin.x, bottom);
    }

    void _OnEnable()
    {

        _color.a = 0;
        UI_Fade.GetComponent<Image>().color = _color;
        UI_Fade.SetActive(true);
        StartCoroutine("Enable");
    }

    void _OnDisable()
    {
        UI_Fade.SetActive(true);
        StartCoroutine("Disable");
    }

    public void OffFide()
    {
        UI_Fade.SetActive(false);
    }


    IEnumerator Disable()
    {
        for (float i = BeginAlpha; i > 0f; i -= (SpeedAlph * 0.015f))
        {
            _color.a = i;
            UI_Fade.GetComponent<Image>().color = _color;
            yield return null;
        }
        UI_Fade.gameObject.SetActive(false);
    }

    IEnumerator Enable()
    {
        for (float i = 0; i < BeginAlpha; i += (SpeedAlph * 0.015f))
        {
            _color.a = i;
            UI_Fade.GetComponent<Image>().color = _color;
            yield return null;
        }
    }

}
