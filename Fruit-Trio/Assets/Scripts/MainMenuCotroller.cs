using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCotroller : MonoBehaviour
{
    public TextMeshProUGUI level;
    public GameObject volumeOff;
    // Start is called before the first frame update
    void Start()
    {
        
        level.text = "LEVEL: " + PlayerPrefs.GetInt("level",1);

        if (!GetData.instance.volume)
        {
            volumeOff.SetActive(true);
        }
    }

    public void ClickStart()
    {
        SceneManager.LoadScene("GamePlay");
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
}
