using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ConsonantSoundModel : MonoBehaviour {
    private static readonly int[,] groupSoundConsonant = new int[,]
    {
        { 1, 8, 0, 0},
        { 14, 15, 20, 21},
        { 26, 27, 43, 0},
        { 2, 3, 0, 0},
        { 16, 22, 0, 0},
        { 28, 29, 0, 0},
        { 38, 39, 40, 0},
        { 9, 41, 0, 0},
        { 4, 5, 6, 0},
        { 10, 11, 12, 44},
        { 17, 18, 23, 24},
        { 30, 31, 32, 0},
        { 13, 19, 34, 25},
        { 35, 36, 42, 0},
        { 37, 33, 7, 0}
    };
    private static int currentGroupConsonant = 0;

    // Use this for initialization
    void Start () {
        // load control menu
        LoadControlMenu();

        // load asset data
        LoadDataScene();
    }

    public static void LoadScene()
    {
        SceneManager.LoadScene("consonant-sound");
    }
    
    public void LoadControlMenu()
    {
        // control menu
        if (currentGroupConsonant == 0 || currentGroupConsonant == -1)
        {
            currentGroupConsonant = 0;
            GameObject objPrevBtn = GameObject.Find("btnPrev");
            objPrevBtn.SetActive(false);
        }
        if (currentGroupConsonant == 14 || currentGroupConsonant == 15)
        {
            currentGroupConsonant = 14;
            GameObject objNextBtn = GameObject.Find("btnNext");
            objNextBtn.SetActive(false);
        }
    }

    public void LoadDataScene()
    {
        for (int i = 0; i < groupSoundConsonant.GetLength(1); i++)
        {
            // load text
            Sprite img = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + groupSoundConsonant[currentGroupConsonant, i]);
            GameObject obj = GameObject.Find("imgConsonant" + (i + 1));
            obj.GetComponent<Image>().sprite = img;

            // load sound
            AudioClip soundClip = Resources.Load<AudioClip>(ConsonantModel.consonantSoundPath + groupSoundConsonant[currentGroupConsonant, i]);
            GameObject objSound = GameObject.Find("soundConsonant" + (i + 1));
            objSound.GetComponent<AudioSource>().clip = soundClip;
        }
    }

    public void LoadDataNext()
    {
        currentGroupConsonant = ++currentGroupConsonant;
        LoadScene();
    }

    public void LoadDataPrev()
    {
        currentGroupConsonant = --currentGroupConsonant;
        LoadScene();
    }
}
