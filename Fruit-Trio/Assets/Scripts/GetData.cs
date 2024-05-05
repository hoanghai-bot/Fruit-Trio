using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetData : MonoBehaviour
{
    Camera _cam;
    public static GetData instance;
    private float _point;
    public float point
    {
        get => _point;
        set 
        {
           _point = value;
            PlayerPrefs.SetFloat("point", point);
        }
    }
    private int _level;
    public int level
    {
        get => _level;
        set
        {
            _level = value;
            PlayerPrefs.SetInt("level", level);
        }
    }
    private int _levelPlayed;
    public int levelPlayed
    {
        get => _levelPlayed;
        set
        {
            _levelPlayed = value;
            PlayerPrefs.SetInt("levelPlayed", levelPlayed);
        }
    }
    private int _coin;
    public int coin
    {
        get => _coin;
        set
        {
            _coin = value;
            PlayerPrefs.SetInt("coin", coin);
        }
    }
    private int _undo;
    public int undo
    {
        get => _undo;
        set
        {
            _undo = value;
            PlayerPrefs.SetInt("undo", undo);
        }
    }
    private int _hint;
    public int hint
    {
        get => _hint;
        set
        {
            _hint = value;
            PlayerPrefs.SetInt("hint", hint);
        }
    }
    private int _suffer;
    public int suffer
    {
        get => _suffer;
        set
        {
            _suffer = value;
            PlayerPrefs.SetInt("suffer", suffer);
        }
    }

    public bool volume;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _cam = Camera.main;
        SetCam();
        volume = true;
        level = PlayerPrefs.GetInt("level",1);
        levelPlayed = PlayerPrefs.GetInt("levelPlayed", 1);
        point = PlayerPrefs.GetFloat("point", 0);
        coin = PlayerPrefs.GetInt("coin", 50);
        undo = PlayerPrefs.GetInt("undo", 5);
        hint = PlayerPrefs.GetInt("hint", 5);
        suffer = PlayerPrefs.GetInt("suffer", 5);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            hint += 50;
            if (GamePlayManager.instance != null)
            {
                GamePlayManager.instance.ShowSP();
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            suffer += 50;
            if (GamePlayManager.instance != null)
            {
                GamePlayManager.instance.ShowSP();
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            undo += 50;
            if (GamePlayManager.instance != null)
            {
                GamePlayManager.instance.ShowSP();
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            levelPlayed += 10;
            
        }
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

    public void SetVolume()
    {
        if (!volume)
        {
            GetComponent<AudioSource>().Play();
            volume = true;
        }
        else
        {
            GetComponent<AudioSource>().Pause();
            volume = false;
        }
    }
}
