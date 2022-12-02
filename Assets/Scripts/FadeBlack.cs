using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBlack : MonoBehaviour
{
    Color _color;
    float BeginAlpha;
    float Speed = 4f;

    private void Awake()
    {
        _color = GetComponent<Image>().color;
        BeginAlpha = _color.a;
    }

    void OnEnable()
    {
        _color.a = BeginAlpha;
        GetComponent<Image>().color = _color;
        StartCoroutine("Disable");
    }

    IEnumerator Disable()
    {
        for (float i = BeginAlpha; i > 0.1f; i -= Speed * 0.015f)
        {
            _color.a = i;
            GetComponent<Image>().color = _color;
            yield return null;
        }
    }

}
