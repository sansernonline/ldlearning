using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;

public class ProfileModel : MonoBehaviour {

    private static string userId = null;
    private static string userFirstName = null;
    private static string[] consonantList = new string[44] 
    {
        "ก", "ข", "ฃ", "ค", "ฅ", "ฆ", "ง", "จ", "ฉ",
        "ช", "ซ", "ฌ", "ญ", "ฎ", "ฏ", "ฐ", "ฑ","ฒ",
        "ณ", "ด", "ต", "ถ", "ท", "ธ", "น","บ", "ป",
        "ผ", "ฝ", "พ", "ฟ", "ภ", "ม", "ย", "ร", "ล",
        "ว", "ศ", "ษ", "ส", "ห", "ฬ", "อ", "ฮ"
    };
    private static string[] vowelList = new string[32]
    {
        "ะ", "า", "ิ", "ี", "ึ", "ื", "ุ", "ู", "เ ะ",
        "เ", "แ ะ", "แ", "โ ะ", "โ", "เ าะ", "อ", "เ อะ","เ อ",
        "เียะ", "เีย", "เือะ", "เือ", "ัะ", "ัว", "ำ","ใ", "ไ",
        "เ า", "ฤ", "ฤๅ", "ฦ", "ฦๅ"};

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name.Equals("main-menu"))
        {
            loadUserInfo();
            GameObject.Find("textProfile").GetComponent<Text>().text = userFirstName;
        }
        else if (SceneManager.GetActiveScene().name.Equals("main-profile"))
        {
            loadUserInfo();
            InputField textField = GameObject.Find("InputFieldName").GetComponent<InputField>();
            textField.text = userFirstName;
            
            // get learning consonant
            LoadLearningStatus(100);

            // get learning vowel
            LoadLearningStatus(200);

            // get learning spelling

            // get learning mark

            // get learning sentence

        }
        else
        {
            loadUserInfo();
        }
    }

    public static void createUser(string name)
    {
        //InputField textField = GameObject.Find("InputFieldName").GetComponent<InputField>();
        //string name = textField.text;

        User u = new User
        {
            Id = userId,
            FirstName = name
        };
        DatabaseConnection.insertUser(u);
    }

    public void updateUser(string name)
    {
        //InputField textField = GameObject.Find("InputFieldName").GetComponent<InputField>();
        //string name = textField.text;

        User u = DatabaseConnection.findUser(userId);
        u.FirstName = name;
        Debug.Log("name : " + name);
        DatabaseConnection.updateUser(u);
    }

    public static void loadUserInfo()
    {
        User u = DatabaseConnection.findUser();
        
        // set user id
        if (u == null)
        {
            userId = Guid.NewGuid().ToString();
            createUser("");
        } else
        {
            userId = u.Id;
            userFirstName = u.FirstName;
        }
    }

    /**
    * course : 100=Consonant, 200=Vowel, 300=Spelling, 400=Mark, 500=Sentence
    * courseDetail : 110=Consonant, 120=Consonant Sound
    */
    public static void saveLearningState(int course, int courseDetail, int currentUnit, int learningTime)
    {
        int courseScore = 0;
        UserCourseScore ucs = null;

        switch (course)
        {
            case 100:    // save consonant data
                // max score
                courseScore = 44;// + 15;

                ucs = new UserCourseScore
                {
                    CourseDetailId = courseDetail,
                    UserId = userId,
                    MyScore = 1,
                    CourseScore = courseScore,
                    LearningTime = learningTime,
                    LearningUnit = currentUnit,
                    Status = 1
                };
                DatabaseConnection.insertUserCourseScore(ucs);
                break;
            case 200:    // save vowel data
                // max score
                courseScore = 32;

                ucs = new UserCourseScore
                {
                    CourseDetailId = courseDetail,
                    UserId = userId,
                    MyScore = 1,
                    CourseScore = courseScore,
                    LearningTime = learningTime,
                    LearningUnit = currentUnit,
                    Status = 1
                };
                DatabaseConnection.insertUserCourseScore(ucs);
                break;
            case 300:
                break;
            case 400:
                break;
            case 500:
                break;
        }
        
    }

    /**
     * examCourse : 100=Consonant, 200=Vowel, 300=Spelling, 400=Mark, 500=Sentence
     * examCourseDetail : 110=Consonant, 120=Consonant Sound
     */
    public static void saveExamState(int examCourse, int examCourseDetail, int currentUnit, int learningTime, int answerScore)
    {
        int courseScore = 0;
        //int maxId = 0;
        //string sql = "";
        ExamScore es = null;

        switch (examCourse)
        {
            case 100:
                // max score
                courseScore = 44;// + 15;

                es = new ExamScore
                {
                    ExamId = examCourseDetail,
                    UserId = userId,
                    MyScore = answerScore,
                    CourseScore = courseScore,
                    LearningTime = learningTime,
                    LearningUnit = currentUnit,
                    Status = 1
                };
                DatabaseConnection.insertExamScore(es);
                break;
            case 200:
                // max score
                courseScore = 32;

                es = new ExamScore
                {
                    ExamId = examCourseDetail,
                    UserId = userId,
                    MyScore = answerScore,
                    CourseScore = courseScore,
                    LearningTime = learningTime,
                    LearningUnit = currentUnit,
                    Status = 1
                };
                DatabaseConnection.insertExamScore(es);
                break;
            case 300:
                break;
            case 400:
                break;
            case 500:
                break;
        }

    }

    public void startLearningTime()
    {

    }

    public void stopLerningTime()
    {

    }

    public void LoadLearningStatus(int courseGroup)
    {
        int latestLesson = 0;
        int countLesson = 0;
        double learningTime = 0;
        double allExamScore = 0;
        double correctExamScore = 0;
        string percent = "";
        string errorUnits = "";
        List<ExamScore> es = null;

        switch (courseGroup)
        {
            case 100:
                // calculate lesson
                latestLesson = DatabaseConnection.getLatestLesson(userId, 110);
                //listResult = DatabaseConnection.getExecuteReader(String.Format(sqlCurrentLesson, userId, "110"));
                GameObject.Find("consonantTopicResult1").GetComponent<Text>().text = String.Format("บทที่ {0}", latestLesson.ToString());

                countLesson = DatabaseConnection.getCountLesson(userId, 110);
                //listResult = DatabaseConnection.getExecuteReader(String.Format(sqlLessonCount, userId, "110"));
                GameObject.Find("consonantTopicResult2").GetComponent<Text>().text = String.Format("{0}/44 บท", countLesson.ToString());

                learningTime = DatabaseConnection.getLearningTime(userId, 110);
                //listResult = DatabaseConnection.getExecuteReader(String.Format(sqlLearningTime, userId, "110"));
                learningTime = Double.Parse(learningTime.ToString());
                GameObject.Find("consonantTopicResult3").GetComponent<Text>().text = String.Format("{0} ชั่วโมง", ((learningTime / 60) / 60).ToString("0.##"));

                // display percent error
                allExamScore = DatabaseConnection.getExamAllScore(userId, 110);
                correctExamScore = DatabaseConnection.getExamCorrectScore(userId, 110);
                //listResult = DatabaseConnection.getExecuteReader(String.Format(sqlPercentErrorUnit, userId, "110"));
                percent = getPercent(allExamScore, correctExamScore);
                GameObject.Find("consonantTopicPercent4").GetComponent<Text>().text = String.Format("{0}%", percent);

                allExamScore = DatabaseConnection.getExamAllScore(userId, 111);
                correctExamScore = DatabaseConnection.getExamCorrectScore(userId, 111);
                //listResult = DatabaseConnection.getExecuteReader(String.Format(sqlPercentErrorUnit, userId, "111"));
                percent = getPercent(allExamScore, correctExamScore);
                GameObject.Find("consonantTopicPercent5").GetComponent<Text>().text = String.Format("{0}%", percent);

                // get most exam error (3 units)
                es = DatabaseConnection.getErrorUnits(userId, 110);
                //listResult = DatabaseConnection.getExecuteReader(String.Format(sqlErrorUnit, userId, "110"));
                errorUnits = getErrorUnits(es, courseGroup);
                GameObject.Find("consonantTopicResult4").GetComponent<Text>().text = String.Format("{0}", errorUnits);
                
                es = DatabaseConnection.getErrorUnits(userId, 111);
                //listResult = DatabaseConnection.getExecuteReader(String.Format(sqlErrorUnit, userId, "111"));
                errorUnits = getErrorUnits(es, courseGroup);
                GameObject.Find("consonantTopicResult5").GetComponent<Text>().text = String.Format("{0}", errorUnits);
                break;
            case 200:
                // calculate lesson
                latestLesson = DatabaseConnection.getLatestLesson(userId, 210);
                GameObject.Find("vowelTopicResult1").GetComponent<Text>().text = String.Format("บทที่ {0}", latestLesson.ToString());

                countLesson = DatabaseConnection.getCountLesson(userId, 210);
                GameObject.Find("vowelTopicResult2").GetComponent<Text>().text = String.Format("{0}/32 บท", countLesson.ToString());

                learningTime = DatabaseConnection.getLearningTime(userId, 210);
                learningTime = Double.Parse(learningTime.ToString());
                GameObject.Find("vowelTopicResult3").GetComponent<Text>().text = String.Format("{0} ชั่วโมง", ((learningTime / 60) / 60).ToString("0.##"));
                
                // display percent error
                allExamScore = DatabaseConnection.getExamAllScore(userId, 211);
                correctExamScore = DatabaseConnection.getExamCorrectScore(userId, 211);
                percent = getPercent(allExamScore, correctExamScore);
                GameObject.Find("vowelTopicPercent4").GetComponent<Text>().text = String.Format("{0}%", percent);

                allExamScore = DatabaseConnection.getExamAllScore(userId, 211);
                correctExamScore = DatabaseConnection.getExamCorrectScore(userId, 211);
                percent = getPercent(allExamScore, correctExamScore);
                GameObject.Find("vowelTopicPercent5").GetComponent<Text>().text = String.Format("{0}%", percent);

                // get most exam error (3 units)
                es = DatabaseConnection.getErrorUnits(userId, 211);
                errorUnits = getErrorUnits(es, courseGroup);
                GameObject.Find("vowelTopicResult4").GetComponent<Text>().text = String.Format("{0}", errorUnits);

                es = DatabaseConnection.getErrorUnits(userId, 211);
                errorUnits = getErrorUnits(es, courseGroup);
                GameObject.Find("vowelTopicResult5").GetComponent<Text>().text = String.Format("{0}", errorUnits);
                 break;
            case 300:
                break;
            case 400:
                break;
            case 500:
                break;
        }
    }

    private static double NewMethod()
    {
        return 0;
    }

    private static string getMapUnitsNo(int no, int courseGroup)
    {
        string result = "";

        switch (courseGroup)
        {
            case 100:
                result = consonantList[no - 1];
                break;
            case 200:
                Debug.Log("no : " +no);
                result = vowelList[no - 1];
                break;
            case 300:
                break;
            case 400:
                break;
            case 500:
                break;
        }

        return result;
    }

    private static string getErrorUnits(List<ExamScore> listResult, int courseGroup)
    {
        string errorUnits = "";
        foreach (ExamScore es in listResult)
        {
            if (!errorUnits.Equals(""))
                errorUnits += String.Format(", {0}", getMapUnitsNo(es.LearningUnit, courseGroup));
            else
                errorUnits += String.Format("{0}", getMapUnitsNo(es.LearningUnit, courseGroup));
        }

        return errorUnits;
    }

    private static string getPercent(double divisor, double set)
    {
        string result = "0";
        if (divisor > 0)
        {
            result = ((set * 100) / divisor).ToString("0.00");
        }

        return result;
    }

    public static string getUserId()
    {
        return userId;
    }

    public static string getUserFirstName()
    {
        return userFirstName;
    }
    
}
