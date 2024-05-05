using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    Camera _cam;
    public int levelEasyer = 0;
    public static GamePlayManager instance;
    [Header("Select Level")]
    public int level ;
    private GameObject levelGame;
    public int maxlevel;
    public GameObject PopUpWin;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI LevelOut;
    public TextMeshProUGUI numberSuffer;
    public TextMeshProUGUI numberUndo;
    public TextMeshProUGUI numberHint;

    public GameObject PopUpLose;

    private int[] listSPItem = new int[3]; //undo, hint, suffer

    public float point = 0;
    public TextMeshProUGUI pointText;

    public GameObject volumeOff;

    public void Awake()
    {
        _cam = Camera.main;
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        level = GetData.instance.level;
        LevelOut.text = "Level: " + level;
        SetCam();
        Application.targetFrameRate = 60;
        LoadLevelGame();

        if (!GetData.instance.volume)
        {
            volumeOff.SetActive(true);
        }
    }

    public void LoadLevelGame()
    {

        levelGame = Instantiate(Resources.Load("NewLevel/Level " + GetData.instance.level) as GameObject,transform);
        ResetLevel();
    }

    private void ResetLevel()
    {
        ShowSP();
        LevelOut.text = "Level: " + GetData.instance.level;
        PopUpWin.GetComponent<MenuWin>().unShow();
        point = 0;
        ShowPoint();
    }

    public void ShowSP()
    {
        numberSuffer.text = GetData.instance.suffer.ToString();
        numberUndo.text = GetData.instance.undo.ToString();
        numberHint.text = GetData.instance.hint.ToString();
    }

    public void ShowPopupWin()
    {
        for (int i = 0; i < listSPItem.Length; i++)
        {
            listSPItem[i] = 0;
        }

        PopUpWin.SetActive(true);
        GetData.instance.level++;
        if (GetData.instance.level > GetData.instance.levelPlayed)
            GetData.instance.levelPlayed = GetData.instance.level;
        Score.text = "Score: " + point;
        float sumPoint = GetData.instance.point + point;
        StartCoroutine(mathForSlide(sumPoint, () => { Debug.Log("CouroutineComplete"); showWin(point); }));

    }


    public int MathCoinForPoint(float point)
    {
        int result = 50;
        float temp = 5000;
        while (temp < point)
        {
            result += 50;
            temp += 5000;
        }

        GetData.instance.coin += result;
        return result;
    }
    

    public IEnumerator mathForSlide(float sumPoint, Action onWin = null)
    {

        for(; sumPoint>10000; sumPoint-=10000)
        {
            Debug.Log("onCourontine");
            PopUpWin.GetComponent<MenuWin>().ControllerSlider(sumPoint,true);
            takeGift();
            yield return new WaitForSeconds(1.5f);
            GetData.instance.point = 0;
        }
        PopUpWin.GetComponent<MenuWin>().ControllerSlider(sumPoint);
        yield return new WaitForSeconds(1f);
        
        GetData.instance.point = sumPoint;
        Debug.Log(GetData.instance.point);
        onWin?.Invoke();
    }

    private void showWin(float point)
    {
        var temp = PopUpWin.GetComponent<MenuWin>();
        temp.showCoin(MathCoinForPoint(point));
        if (listSPItem[0] !=0)
        {
            temp.showUndo(listSPItem[0]);
        }
        if (listSPItem[1] != 0)
        {
            temp.showHint(listSPItem[1]);
        }
        if (listSPItem[2] != 0)
        {
            temp.showSuffer(listSPItem[2]);
        }
    }

    private void takeGift()
    {
        int i;
        i = UnityEngine.Random.Range(0, 3);
        listSPItem[i]++;
        switch (i)
        {
            case 0:
                GetData.instance.undo++;
                break;
            case 1:
                GetData.instance.hint++;
                break;
            case 2:
                GetData.instance.suffer++;
                break;
        }
    }

    public void Replay()
    {
        GetData.instance.level--;
        NextLevel();
    }

    public void NextLevel()
    {
        LevelManager.instance.levelStat = LevelStat.NORMAL; 
        level = GetData.instance.level;
        if (level > maxlevel)
            level = maxlevel;
        Destroy(levelGame);
        LoadLevelGame();
    }
    public void SufferClick()
    {
        if (LevelManager.instance.levelStat == LevelStat.RESTART) return;
        if(GetData.instance.suffer > 0)
        {
            GetData.instance.suffer--;
            levelGame.GetComponent<LevelManager>().Suffer();
            numberSuffer.text = GetData.instance.suffer.ToString();
        }
        else
        {
            return;
        }
        
    }
    public void HintClick()
    {
        if (LevelManager.instance.levelStat == LevelStat.RESTART) return;
        if (GetData.instance.hint > 0)
        {
            GetData.instance.hint--;
            levelGame.GetComponent<LevelManager>().Hint();
            numberHint.text = GetData.instance.hint.ToString();
        }
        else
        {
            return;
        }
    }
    public void UndoClick()
    {
        if (LevelManager.instance.levelStat == LevelStat.RESTART) return;
        if (GetData.instance.undo > 0 && LevelManager.instance.listUndo.Count>0)
        {
            GetData.instance.undo--;
            levelGame.GetComponent<LevelManager>().Undo();
            numberUndo.text = GetData.instance.undo.ToString();
        }
        else
        {
            return;
        }
    }

    public void ClickHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ClickVolume(GameObject obj)
    {
        GetData.instance.SetVolume();
        if (GetData.instance.volume)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }

    public void SetLevelStatRestart()
    {
        LevelManager.instance.levelStat = LevelStat.RESTART;
    }

    public void SetLevelStatNormal()
    {
        LevelManager.instance.levelStat = LevelStat.NORMAL;
    }

    void SetCam()
    {
        if (Screen.height >= 1920 * Mathf.RoundToInt(Screen.dpi / 160))
        {
            // _size = 2.65f;
            _cam.orthographicSize = 10f;
        }
        else
        {
            // _size = 2.2f;
            _cam.orthographicSize = 12f;
        }
    }

    public void ShowPoint()
    {
        pointText.text = "Score: " + point;
    }
}

