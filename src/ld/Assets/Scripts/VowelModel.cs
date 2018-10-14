using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VowelModel : MonoBehaviour {

    private static int currentVowelNo = 0;
    public const string vowelImgPath = "Vowel/";
    public const string vowelTextPath = "Vowel/Text/";
    public const string vowelSoundPath = "Vowel/Sound/";

    // Use this for initialization
    void Start()
    {
        // load control menu
        LoadControlMenu();

        // load asset data
        LoadDataScene();

        // save learning state
        ProfileModel.saveLearningState(200, 210, currentVowelNo, 60);
    }

    public static void LoadScene(int vowelNo)
    {
        currentVowelNo = vowelNo;

        SceneManager.LoadScene("vowel-text");
    }

    public void LoadDataScene() {
        // active when stay in vowel-text scene
        if (SceneManager.GetActiveScene().name.Equals("vowel-text"))
        {
            // load text
            Sprite txtTexture = Resources.Load<Sprite>(vowelTextPath + "v" + currentVowelNo);
            GameObject objTxt = GameObject.Find("imgText");
            objTxt.GetComponent<Image>().sprite = txtTexture;

            // load sound
            AudioClip soundClip = Resources.Load<AudioClip>(vowelSoundPath + currentVowelNo);
            GameObject objSound = GameObject.Find("soundSource");
            objSound.GetComponent<AudioSource>().clip = soundClip;
        }
    }

    private void LoadControlMenu()
    {
        Debug.Log("LoadDataScene");

        try
        {
            if (currentVowelNo == 1 || currentVowelNo == 0)
            {
                currentVowelNo = 1;
                GameObject objPrevBtn = GameObject.Find("btnPrev");
                //objPrevBtn.GetComponent<Renderer>().enabled = false;
                objPrevBtn.SetActive(false);
            }
            if (currentVowelNo == 32 || currentVowelNo == 33)
            {
                currentVowelNo = 32;
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
        currentVowelNo = ++currentVowelNo;

        Debug.Log("next : " + currentVowelNo);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadDataPrev()
    {
        currentVowelNo = --currentVowelNo;

        Debug.Log("prev : " + currentVowelNo);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }    
}
