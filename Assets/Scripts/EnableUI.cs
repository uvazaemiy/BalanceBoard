using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnableUI : MonoBehaviour
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
        _color.a = 0;
        GetComponent<Image>().color = _color;
        
        StartCoroutine("Enable");
    }

    public void _OnDisable()
    {
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
        gameObject.SetActive(false);
    }

    IEnumerator Enable()
    {
        for (float i = 0; i < BeginAlpha; i += Speed * 0.015f)
        {
            _color.a = i;
            GetComponent<Image>().color = _color;
            yield return null ;
        }
    }

   
}
