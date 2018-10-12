using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ConsonantExamModel : MonoBehaviour {

    private static readonly int[,] groupConsonant = new int[,] 
    { 
        { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 
        {10, 11, 12, 13, 14, 15, 16, 17, 18 }, 
        {19, 20, 21, 22, 23, 24, 25, 26, 27 },
        {28, 29,30, 31, 32, 33, 34, 35, 36 },
        {37, 38, 39, 40, 41, 42, 43, 44, 45 }
    };
    private static readonly int[] groupExam = new int[] { 1, 2, 3 };
    private static int chooseConsonantGroup = 0;
    private static int randomConsonantNo = 0;
    private static int randomExamNo = 0;
    private static int choiceCorrectIndex = 0;

    // Use this for initialization
    void Start () {
		//if (!SceneManager.GetActiveScene().name.Equals("consonant-text-test-menu"))
        //{
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
        //}
	}
	
    public static void LoadScene(int consonantGroupNo)
    {
        // random consonant
        chooseConsonantGroup = consonantGroupNo;
        randomConsonantNo = RandomConsonant(chooseConsonantGroup);

        // random exam
        randomExamNo = RandomExam();

        // load scene exam
        // TODO : remove it 
        //randomExamNo = 2;
        Debug.Log("consonant no : " + randomExamNo);
        SceneManager.LoadScene("consonant-text-test-" + randomExamNo);
    }
    
    // ==========================================================================
    //                             Exam 1
    // ==========================================================================
    public static void LoadDataSceneExam1()
    {
        Debug.Log("load component ex1");

        // load image question
        Sprite imgQuestion = Resources.Load<Sprite>(ConsonantModel.consonantImgPath + randomConsonantNo);
        GameObject objQuestion = GameObject.Find("imgConsonant");
        objQuestion.GetComponent<Image>().sprite = imgQuestion;
        // load sound question
        AudioClip soundClip = Resources.Load<AudioClip>(ConsonantModel.consonantSoundPath + randomConsonantNo);
        GameObject objSound = GameObject.Find("soundConsonant");
        objSound.GetComponent<AudioSource>().clip = soundClip;
        
        int[] choices = RandomChoices(3);
        // load text1 answer
        Sprite imgAns1 = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + choices[0]);
        GameObject objAns1 = GameObject.Find("imgConsonantText1");
        objAns1.GetComponent<Image>().sprite = imgAns1;
        // load text2 answer
        Sprite imgAns2 = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + choices[1]);
        GameObject objAns2 = GameObject.Find("imgConsonantText2");
        objAns2.GetComponent<Image>().sprite = imgAns2;
        // load text3 answer
        Sprite imgAns3 = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + choices[2]);
        GameObject objAns3 = GameObject.Find("imgConsonantText3");
        objAns3.GetComponent<Image>().sprite = imgAns3;
    }

    public void ChooseChoiceExam1(int choice)
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
            }

            // save score
            ProfileModel.saveExamState(100, 110, randomConsonantNo, 60, 1);
        }
        else // play sound wrong
        {
            GameObject objSound = GameObject.Find("soundWrong");
            objSound.GetComponent<AudioSource>().Play();

            // save score
            ProfileModel.saveExamState(100, 110, randomConsonantNo, 60, 0);
        }
    }

    public void LoadNextExam()
    {
        LoadScene(chooseConsonantGroup);
    }

    // ==========================================================================
    //                             Exam 2
    // ==========================================================================
    public static void LoadDataSceneExam2()
    {
        Debug.Log("load component ex2");

        // load text question
        Sprite imgQuestion = Resources.Load<Sprite>(ConsonantModel.consonantTextPath + "t" + randomConsonantNo);
        GameObject objQuestion = GameObject.Find("imgConsonantText");
        objQuestion.GetComponent<Image>().sprite = imgQuestion;
        // load sound question
        AudioClip soundClip = Resources.Load<AudioClip>(ConsonantModel.consonantSoundPath + randomConsonantNo);
        GameObject objSound = GameObject.Find("soundConsonant");
        objSound.GetComponent<AudioSource>().clip = soundClip;

        int[] choices = RandomChoices(4);
        // load image1 answer
        Sprite imgAns1 = Resources.Load<Sprite>(ConsonantModel.consonantImgPath + choices[0]);
        GameObject objAns1 = GameObject.Find("imgConsonant1");
        objAns1.GetComponent<Image>().sprite = imgAns1;
        // load image2 answer
        Sprite imgAns2 = Resources.Load<Sprite>(ConsonantModel.consonantImgPath + choices[1]);
        GameObject objAns2 = GameObject.Find("imgConsonant2");
        objAns2.GetComponent<Image>().sprite = imgAns2;
        // load image3 answer
        Sprite imgAns3 = Resources.Load<Sprite>(ConsonantModel.consonantImgPath + choices[2]);
        GameObject objAns3 = GameObject.Find("imgConsonant3");
        objAns3.GetComponent<Image>().sprite = imgAns3;
        // load image4 answer
        Sprite imgAns4 = Resources.Load<Sprite>(ConsonantModel.consonantImgPath + choices[3]);
        GameObject objAns4 = GameObject.Find("imgConsonant4");
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
            ProfileModel.saveExamState(100, 111, randomConsonantNo, 60, 1);
        }
        else // play sound wrong
        {
            GameObject objSound = GameObject.Find("soundWrong");
            objSound.GetComponent<AudioSource>().Play();

            // save score
            ProfileModel.saveExamState(100, 111, randomConsonantNo, 60, 0);
        }
    }

    // ==========================================================================
    //                             Exam 3
    // ==========================================================================
    public static void LoadDataSceneExam3()
    {

    }

    private static int RandomConsonant(int consonantGroupNo)
    {
        System.Random rnd = new System.Random();
        int result = 0;
        bool loop = true;
        
        // random all consonant
        if (consonantGroupNo == 5)
        {
            result = rnd.Next(1, 45);
        }
        else // random group consonant
        {
            while (loop)
            {
                result = rnd.Next(groupConsonant[consonantGroupNo, 0], groupConsonant[consonantGroupNo, 8]);

                if ((result >= 1 && result <= 44) && result != randomConsonantNo)
                {
                    loop = false;
                }
            }
        }
        
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
        Debug.Log("random consonant : " + randomConsonantNo);

        choice[choiceCorrectIndex] = randomConsonantNo;

        // random choice data
        bool loop = true;

        while (loop)
        {
            int randData = RandomConsonant(chooseConsonantGroup);
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
