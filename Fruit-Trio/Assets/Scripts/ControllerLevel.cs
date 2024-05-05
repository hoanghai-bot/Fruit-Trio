using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerLevel : MonoBehaviour
{
    
    private int countData = 0;
    public GameObject prefabButton;
    public Transform content;
    // Start is called before the first frame update
    void Start()
    {
        CheckData();
        for (int i = 0; i < countData; i++) 
        {
            var temp = Instantiate(prefabButton,content);
            temp.SetActive(true);
            temp.GetComponentInChildren<TextMeshProUGUI>().text = (i+1).ToString();
            if((i+1) > GetData.instance.levelPlayed)
            {
                
                temp.transform.Find("lock").gameObject.SetActive(true);
                temp.GetComponent<Button>().enabled = false;
            }
            else
            {
                temp.transform.Find("lock").gameObject.SetActive(false);
                temp.GetComponent<Button>().enabled = true;
            }
        }
        
    }

    

    public void ActiveLevel(TextMeshProUGUI text)
    {
        GetData.instance.level = int.Parse(text.text);
        SceneManager.LoadScene("GamePlay");
    }

    private void CheckData()
    {

         // Số lượng tệp có định dạng không phải là meta
         var files = Resources.LoadAll("NewLevel");
         foreach (var file in files)
         {
            
                 countData++;
         }
        
         Debug.Log("Số lượng tài nguyên trong thư mục Resources là: " + countData);
   
    }
}
