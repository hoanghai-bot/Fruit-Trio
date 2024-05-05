using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Security.Cryptography;
using TMPro;

public class MenuWin : MonoBehaviour
{
    public Slider slider;
    public GameObject coin;
    public GameObject undo;
    public GameObject hint;
    public GameObject suffer;
    
    private void Start()
    {
        
    }

    public void ControllerSlider(float point ,bool isStartVal0=false)
    {
        float valStart;
        if (isStartVal0)
        {
             valStart = 0;
        }
        else
        {
             valStart = GetData.instance.point/10000 ;
        }

        float valEnd = Mathf.Clamp01( GetData.instance.point/10000 + point/10000);

        Debug.Log(valEnd);

        slider.value = valStart;
        slider.DOValue(valEnd, 1f);
        
    }

    public void showCoin(int number)
    {
        coin.SetActive(true);
        coin.GetComponent<TextMeshProUGUI>().text = "+"+number;
    }

    public void showUndo(int number)
    {
        undo.SetActive(true);
        undo.GetComponent<TextMeshProUGUI>().text = "+" + number;
    }
    public void showHint(int number)
    {
        hint.SetActive(true);
        hint.GetComponent<TextMeshProUGUI>().text = "+" + number;
    }
    public void showSuffer(int number)
    {
        suffer.SetActive(true);
        suffer.GetComponent<TextMeshProUGUI>().text = "+" + number;
    }
    public void unShow()
    {
        coin.SetActive(false);
        undo.SetActive(false);
        hint.SetActive(false);
        suffer.SetActive(false );
    }
}
