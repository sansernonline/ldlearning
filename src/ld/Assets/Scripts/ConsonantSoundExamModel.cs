using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConsonantSoundExamModel : MonoBehaviour {

    private static int randomConsonantNo = 0;
    private static int choiceCorrectIndex = 0;

    // Use this for initialization
    void Start () {
        // load asset data
        LoadDataScene();
    }

    public static void LoadScene()
    {
        // random consonant
        randomConsonantNo = RandomConsonant();
       
        // load scene exam
        SceneManager.LoadScene("consonant-sound-test");
    }

    public static void LoadDataScene()
    {
        Debug.Log("load component ex1");
        // load sound question
        AudioClip soundClip = Resources.Load<AudioClip>(ConsonantModel.consonantSoundPath + randomConsonantNo);
        GameObject objSound = GameObject.Find("soundConsonant");
        objSound.GetComponent<AudioSource>().clip = soundClip;

        int[] choices = RandomChoices(4);
        // load text1 answer
        Sprite imgAns1 = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + choices[0]);
        GameObject objAns1 = GameObject.Find("imgConsonant1");
        objAns1.GetComponent<Image>().sprite = imgAns1;
        // load text2 answer
        Sprite imgAns2 = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + choices[1]);
        GameObject objAns2 = GameObject.Find("imgConsonant2");
        objAns2.GetComponent<Image>().sprite = imgAns2;
        // load text3 answer
        Sprite imgAns3 = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + choices[2]);
        GameObject objAns3 = GameObject.Find("imgConsonant3");
        objAns3.GetComponent<Image>().sprite = imgAns3;
        // load text4 answer
        Sprite imgAns4 = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + choices[3]);
        GameObject objAns4 = GameObject.Find("imgConsonant4");
        objAns4.GetComponent<Image>().sprite = imgAns4;
    }

    public void ChooseChoiceExam(int choice)
    {
        if (choice == choiceCorrectIndex)
        {
            // play sound correct
            GameObject objSound = GameObject.Find("soundCorrect");
            objSound.GetComponent<AudioSource>().Play();

            // show circle
            switch (choice)
            {
                case 0:
                    Sprite[] imgTexture1 = Resources.LoadAll<Sprite>("circle-line");
                    GameObject objImg1 = GameObject.Find("circle1");
                    objImg1.GetComponent<Image>().sprite = imgTexture1[0];
                    break;
                case 1:
                    Sprite[] imgTexture2 = Resources.LoadAll<Sprite>("circle-line");
                    GameObject objImg2 = GameObject.Find("circle2");
                    objImg2.GetComponent<Image>().sprite = imgTexture2[0];
                    break;
                case 2:
                    Sprite[] imgTexture3 = Resources.LoadAll<Sprite>("circle-line");
                    GameObject objImg3 = GameObject.Find("circle3");
                    objImg3.GetComponent<Image>().sprite = imgTexture3[0];
                    break;
                case 3:
                    Sprite[] imgTexture4 = Resources.LoadAll<Sprite>("circle-line");
                    GameObject objImg4 = GameObject.Find("circle4");
                    objImg4.GetComponent<Image>().sprite = imgTexture4[0];
                    break;
            }
        }
        else // play sound wrong
        {
            GameObject objSound = GameObject.Find("soundWrong");
            objSound.GetComponent<AudioSource>().Play();
        }
    }

    public void LoadDataNext()
    {
        // random consonant
        randomConsonantNo = RandomConsonant();

        LoadScene();
    }
    
    private static int RandomConsonant()
    {
        System.Random rnd = new System.Random();
        int result = 0;

        // random all consonant
        result = rnd.Next(1, 45);
        
        return result;
    }

    private static int[] RandomChoices(int answers)
    {
        System.Random rnd = new System.Random();

        int[] choice = new int[answers];
        for (int i = 0; i < answers; i++)
        {
            choice[i] = 0;
        }
        choiceCorrectIndex = rnd.Next(0, answers);

        //Debug.Log("Random Choices");
        Debug.Log("random index : " + choiceCorrectIndex);
        Debug.Log("random consonant : " + randomConsonantNo);

        choice[choiceCorrectIndex] = randomConsonantNo;

        // random choice data
        bool loop = true;

        while (loop)
        {
            int randData = RandomConsonant();
            int addChoiceIndex = -1;

            for (int i = 0; i < choice.Length; i++)
            {
                if (choice[i] == randData)
                {
                    addChoiceIndex = -1;
                    break;
                }
                else if (choice[i] == 0)
                {
                    addChoiceIndex = i;
                }
            }

            // set choice to array
            if (addChoiceIndex != -1)
                choice[addChoiceIndex] = randData;

            // check all choice
            loop = false;
            for (int i = 0; i < choice.Length; i++)
            {
                if (choice[i] == 0)
                    loop = true;
            }

        }

        return choice;
    }
}
