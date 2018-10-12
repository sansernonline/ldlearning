using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    void Start()
    {

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // load consonant scene
    public void LoadSceneConsonant(int consonantNo)
    {
        ConsonantModel.LoadScene(consonantNo);
    }
    // load consonant exam scene
    public void LoadSceneConsonantExam(int groupConsonant)
    {
        ConsonantExamModel.LoadScene(groupConsonant);
    }
    // load consonant sound scene
    public void LoadSceneConsonantSound()
    {
        ConsonantSoundModel.LoadScene();
    }
    // load consonant sound exam scene
    public void LoadSceneConsonantSoundExam()
    {
        ConsonantSoundExamModel.LoadScene();
    }

    // load vowel scene
    public void LoadSceneVowel(int vowelNo)
    {
        VowelModel.LoadScene(vowelNo);
    }
    // load consonant exam scene
    public void LoadSceneVowelExam()
    {
        VowelExamModel.LoadScene();
    }
}
