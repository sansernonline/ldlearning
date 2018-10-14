using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConsonantModel : MonoBehaviour {

    private static int currentConsonantNo = 0;
    //private static int consonantNoNext = 0;
    //private static int consonantNoPrev = 0;
    public const string consonantImgPath = "Consonant/";
    public const string consonantTextPath = "Consonant/Text/";
    public const string consonantSoundPath = "Consonant/Sound/";

    // Use this for initialization
    void Start()
    {
        // load control menu
        LoadControlMenu();

        // load asset data
        LoadDataScene();

        // save learning state
        ProfileModel.saveLearningState(100, 110, currentConsonantNo, 60);
    }

    public static void LoadScene(int consonantNo)
    {
        currentConsonantNo = consonantNo;

        SceneManager.LoadScene("consonant-text");
    }

    public void LoadDataScene() {
        // active when stay in consonant-text scene
        if (SceneManager.GetActiveScene().name.Equals("consonant-text"))
        {
            //Debug.Log("scene : " + SceneManager.GetActiveScene().name);
            //Debug.Log("con no : " + consonantNo);

            // load image
            Sprite imgTexture = Resources.Load<Sprite>(consonantImgPath + currentConsonantNo);
            //Debug.Log("path : " + consonantImgPath + consonantNo);
            //Debug.Log("img : " + imgTexture.name);
            GameObject objImg = GameObject.Find("imgTopic");
            objImg.GetComponent<Image>().sprite = imgTexture;

            // load text
            Sprite txtTexture = Resources.Load<Sprite>(consonantTextPath + "t" + currentConsonantNo);
            GameObject objTxt = GameObject.Find("imgText");
            objTxt.GetComponent<Image>().sprite = txtTexture;

            // load sound
            AudioClip soundClip = Resources.Load<AudioClip>(consonantSoundPath + currentConsonantNo);
            GameObject objSound = GameObject.Find("soundSource");
            objSound.GetComponent<AudioSource>().clip = soundClip;
        }
    }

    private void LoadControlMenu()
    {
        Debug.Log("LoadDataScene");

        try
        {
            if (currentConsonantNo == 1 || currentConsonantNo == 0)
            {
                currentConsonantNo = 1;
                GameObject objPrevBtn = GameObject.Find("btnPrev");
                //objPrevBtn.GetComponent<Renderer>().enabled = false;
                objPrevBtn.SetActive(false);
            }
            if (currentConsonantNo == 44 || currentConsonantNo == 45)
            {
                currentConsonantNo = 44;
                GameObject objNextBtn = GameObject.Find("btnNext");
                objNextBtn.SetActive(false);
            }
        } catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void LoadDataNext()
    {
        currentConsonantNo = ++currentConsonantNo;

        Debug.Log("next : " + currentConsonantNo);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadDataPrev()
    {
        currentConsonantNo = --currentConsonantNo;

        Debug.Log("prev : " + currentConsonantNo);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
