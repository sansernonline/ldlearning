using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class VowelExamModel : MonoBehaviour
{
    private static readonly int[] groupExam = new int[] { 1, 2, 3 };
    private static int randomVowelNo = 0;
    private static int randomExamNo = 0;
    private static int choiceCorrectIndex = 0;

    // Use this for initialization
    void Start () {
        Debug.Log("start");

        switch (randomExamNo)
        {
            case 1:
                LoadDataSceneExam1();
                break;
            case 2:
                LoadDataSceneExam2();
                break;
            case 3:
                LoadDataSceneExam3();
                break;
        }
	}
	
    public static void LoadScene()
    {
        // random vowel
        randomVowelNo = RandomVowelNo();

        // random exam
        //randomExamNo = RandomExam();

        // load scene exam
        // TODO : remove it 
        randomExamNo = 2;
        SceneManager.LoadScene("vowel-text-test-" + randomExamNo);
    }
    
    // ==========================================================================
    //                             Exam 1
    // ==========================================================================
    public static void LoadDataSceneExam1()
    {

    }

    public void ChooseChoiceExam1(int choice)
    {
        
    }

    public void LoadNextExam()
    {
        LoadScene();
    }

    // ==========================================================================
    //                             Exam 2
    // ==========================================================================
    public static void LoadDataSceneExam2()
    {
        Debug.Log("load component ex2");
        
        // load sound question
        AudioClip soundClip = Resources.Load<AudioClip>(VowelModel.vowelSoundPath + randomVowelNo);
        GameObject objSound = GameObject.Find("soundControl");
        objSound.GetComponent<AudioSource>().clip = soundClip;

        int[] choices = RandomChoices(4);
        // load image1 answer
        Sprite imgAns1 = Resources.Load<Sprite>(VowelModel.vowelTextPath + "v" + choices[0]);
        GameObject objAns1 = GameObject.Find("imgChoice1");
        objAns1.GetComponent<Image>().sprite = imgAns1;
        // load image2 answer
        Sprite imgAns2 = Resources.Load<Sprite>(VowelModel.vowelTextPath + "v" + choices[1]);
        GameObject objAns2 = GameObject.Find("imgChoice2");
        objAns2.GetComponent<Image>().sprite = imgAns2;
        // load image3 answer
        Sprite imgAns3 = Resources.Load<Sprite>(VowelModel.vowelTextPath + "v" + choices[2]);
        GameObject objAns3 = GameObject.Find("imgChoice3");
        objAns3.GetComponent<Image>().sprite = imgAns3;
        // load image4 answer
        Sprite imgAns4 = Resources.Load<Sprite>(VowelModel.vowelTextPath + "v" + choices[3]);
        GameObject objAns4 = GameObject.Find("imgChoice4");
        objAns4.GetComponent<Image>().sprite = imgAns4;
    }

    public void ChooseChoiceExam2(int choice)
    {
        Debug.Log("ChooseChoiceExam2 - correct index : " + choiceCorrectIndex);
        Debug.Log("ChooseChoiceExam2 - choice : " + choice);

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

            // save score
            ProfileModel.saveExamState(200, 211, randomVowelNo, 60, 1);
        }
        else // play sound wrong
        {
            GameObject objSound = GameObject.Find("soundWrong");
            objSound.GetComponent<AudioSource>().Play();

            // save score
            ProfileModel.saveExamState(200, 211, randomVowelNo, 60, 0);
        }
    }

    // ==========================================================================
    //                             Exam 3
    // ==========================================================================
    public static void LoadDataSceneExam3()
    {

    }

    private static int RandomVowelNo()
    {
        System.Random rnd = new System.Random();
        int result = 0;
        
        result = rnd.Next(1, 33);
        
        return result;
    }

    private static int RandomExam()
    {
        System.Random rnd = new System.Random();
        // TODO : change
        // int result = rnd.Next(groupExam[0], groupExam[groupExam.Length-1]+1);
        int result = rnd.Next(groupExam[0], groupExam[groupExam.Length-1]);

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
        Debug.Log("random vowel : " + randomVowelNo);

        choice[choiceCorrectIndex] = randomVowelNo;

        // random choice data
        bool loop = true;

        while (loop)
        {
            int randData = RandomVowelNo();
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
