using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

    //public GameObject ls;
    public GameObject loadScreen;
    public Scrollbar scrollbar;

    //to disable
    public GameObject menuScreen; 

    //individual calls
    //public Text loadText;
    //public Image im1;
    //public Image im2;
    //public Image im3;
    //public Image im4; 

    void Start()
    {
        loadScreen.SetActive(false);
        menuScreen.SetActive(true);

        //diable all text images
        //loadText.enabled = false;
        //im1.enabled = false;
        //im2.enabled = false;
        //im3.enabled = false;
        //im4.enabled = false;

    }

    public void loadScene(string scenename)
    {
        Debug.Log("button pressed");
        //SetLoadScreenActive();

        //Application.LoadLevel(scenename);

        StartCoroutine(LoadAsynch(scenename));

    }

    IEnumerator LoadAsynch(string scenename){

        AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);

        menuScreen.SetActive(false);
        loadScreen.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            scrollbar.size = progress; 
            Debug.Log(progress);

            yield return null;
         
        }

    }

    public void SetLoadScreenActive()
    {
        //loadC.enabled = true;
        //loadText.enabled = true;
        //im1.enabled = true;
        //im2.enabled = true;
        //im3.enabled = true;
        //im4.enabled = true;

    }

}
