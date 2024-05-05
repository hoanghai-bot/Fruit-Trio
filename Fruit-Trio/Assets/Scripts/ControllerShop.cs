using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ControllerShop : MonoBehaviour
{
    public TextMeshProUGUI numberUndo;
    public TextMeshProUGUI numberHint;
    public TextMeshProUGUI numberSuffer;
    public TextMeshProUGUI coin;

    public TextMeshProUGUI coinUndo;
    public TextMeshProUGUI coinHint;
    public TextMeshProUGUI coinSuffer;
    [Header("-------price-------")]
    public int priceUndo;
    public int priceHint;
    public int priceSuffer;
    // Start is called before the first frame update
    void Start()
    {
        coinUndo.text = priceUndo.ToString();
        coinHint.text = priceHint.ToString();
        coinSuffer.text = priceSuffer.ToString();
        UpdateNumber();
    }

    public void UpdateNumber()
    {
        numberUndo.text = GetData.instance.undo.ToString();
        numberHint.text = GetData.instance.hint.ToString();
        numberSuffer.text = GetData.instance.suffer.ToString();
        coin.text = GetData.instance.coin.ToString();
    }

    public void UndoClick()
    {
        if(GetData.instance.coin >= priceUndo)
        {
            GetData.instance.coin -= priceUndo;
            GetData.instance.undo ++;
            UpdateNumber();
        }
    }
    public void HintClick()
    {
        if (GetData.instance.coin >= priceHint)
        {
            GetData.instance.coin -= priceHint;
            GetData.instance.hint++;
            UpdateNumber();
        }
    }
    public void SufferClick()
    {
        if (GetData.instance.coin >= priceSuffer)
        {
            GetData.instance.coin -= priceSuffer;
            GetData.instance.suffer++;
            UpdateNumber();
        }
    }
}
