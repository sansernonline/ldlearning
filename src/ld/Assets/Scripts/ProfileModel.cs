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

    // Use this for initialization
    void Start () {

        if (SceneManager.GetActiveScene().name.Equals("main-menu"))
        {
            loadUserInfo();
            GameObject.Find("textProfile").GetComponent<Text>().text = userFirstName;
        } else if (SceneManager.GetActiveScene().name.Equals("main-profile"))
        {
            loadUserInfo();
            InputField textField = GameObject.Find("InputFieldName").GetComponent<InputField>();
            textField.text = userFirstName;

            // get learning status
            getLearningStatus(100);
        }
    }

    public static void createUser(string name)
    {
        //InputField textField = GameObject.Find("InputFieldName").GetComponent<InputField>();
        //string name = textField.text;

        string sql = string.Format("insert into User (Id, FirstName) values ('{0}','{1}')", userId, name);
        DatabaseConnection.getExecuteScalar(sql);
    }

    public void updateUser(string name)
    {
        //InputField textField = GameObject.Find("InputFieldName").GetComponent<InputField>();
        //string name = textField.text;

        string sql = string.Format("update User set FirstName='{0}' where Id='{1}'", name, userId);
        DatabaseConnection.getExecuteScalar(sql);
    }

    public static void loadUserInfo()
    {
        // select user from database
        string sql = "SELECT Id, FirstName FROM User ";
        List<Dictionary<String, System.Object>> listResult = DatabaseConnection.getExecuteReader(sql);
        
        foreach (Dictionary<String, System.Object> map in listResult)
        {
            //foreach (KeyValuePair<String, System.Object> item in map)
            //{
            //    Debug.Log(String.Format("Key: {0}, Value: {1}", item.Key, item.Value));
            //}

            userId = map["Id"].ToString();
            userFirstName = map["FirstName"].ToString();
        }

        // set user id
        if (userId == null)
        {
            userId = Guid.NewGuid().ToString();
            createUser("");
        }


        // get consonant info


        // get vowel info


        // get spelling info


        // get mark info
    }

    /**
    * course : 100=Consonant, 200=Vowel, 300=Spelling, 400=Mark, 500=Sentence
    * courseDetail : 110=Consonant, 120=Consonant Sound
    */
    public static void saveLearningState(int course, int courseDetail, int currentUnit, int learningTime)
    {
        int courseScore = 0;

        switch (course)
        {
            case 100:

                // max score
                courseScore = 44;// + 15;
                
                // get max id
                int maxId = DatabaseConnection.getMaxId("UserCourseScore");

                string sql = string.Format("insert into UserCourseScore (Id, CourseDetailId, UserId, MyScore, CourseScore, LearningTime, LearningUnit, Status) " +
                    "values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", maxId, courseDetail, userId, 1, courseScore, learningTime, currentUnit, 1);
                DatabaseConnection.getExecuteScalar(sql);

                break;
            case 200:
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
        
        switch (examCourse)
        {
            case 100:
                // max score
                courseScore = 44;// + 15;

                // get max id
                int maxId = DatabaseConnection.getMaxId("ExamScore");

                string sql = string.Format("insert into ExamScore (Id, ExamId, UserId, MyScore, CourseScore, LearningTime, LearningUnit, Status) " +
                    "values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", maxId, examCourseDetail, userId, answerScore, courseScore, learningTime, currentUnit, 1);
                DatabaseConnection.getExecuteScalar(sql);

                break;
            case 200:
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

    public void getLearningStatus(int courseGroup)
    {
        // lesson
        string sqlCurrentLesson = "SELECT IFNULL(MAX(LearningUnit),0) as currentUnit FROM UserCourseScore WHERE userId='{0}' AND CourseDetailId='{1}' ";
        string sqlLessonCount = "SELECT IFNULL(COUNT(distinct LearningUnit),0) as countUnit FROM UserCourseScore WHERE userId='{0}' AND CourseDetailId='{1}' ";
        string sqlLearningTime = "SELECT IFNULL(SUM(LearningTime),0) as learningTime FROM UserCourseScore WHERE userId='{0}' AND CourseDetailId='{1}' ";

        // exam
        string sqlPercentErrorUnit = "SELECT IFNULL(COUNT(MyScore),0) AS allExam, IFNULL(sum(MyScore),0) AS correctExam FROM ExamScore WHERE UserId='{0}' AND ExamId='{1}' ";

        // error unit
        string sqlErrorUnit = "SELECT LearningUnit AS errorUnit, IFNULL(COUNT(MyScore),0) AS wrongCount FROM ExamScore WHERE UserId='{0}' AND ExamId='{1}' AND MyScore=0 GROUP BY LearningUnit ORDER BY wrongCount DESC LIMIT 3 ";

        switch (courseGroup)
        {
            case 100:
                // calculate lesson
                List<Dictionary<String, System.Object>> listResult = null;
                listResult = DatabaseConnection.getExecuteReader(String.Format(sqlCurrentLesson, userId, "110"));
                GameObject.Find("consonantTopicResult1").GetComponent<Text>().text = String.Format("บทที่ {0}", listResult[0]["currentUnit"].ToString());
                
                listResult = DatabaseConnection.getExecuteReader(String.Format(sqlLessonCount, userId, "110"));
                GameObject.Find("consonantTopicResult2").GetComponent<Text>().text = String.Format("{0}/44 บท", listResult[0]["countUnit"].ToString());
                
                listResult = DatabaseConnection.getExecuteReader(String.Format(sqlLearningTime, userId, "110"));
                double learningTime = Double.Parse(listResult[0]["learningTime"].ToString());
                GameObject.Find("consonantTopicResult3").GetComponent<Text>().text = String.Format("{0} ชั่วโมง", ((learningTime / 60) / 60).ToString("0.##"));
                
                // display percent error
                listResult = DatabaseConnection.getExecuteReader(String.Format(sqlPercentErrorUnit, userId, "110"));
                string percent = "";
                percent = getPercent(Double.Parse(listResult[0]["allExam"].ToString()), Double.Parse(listResult[0]["correctExam"].ToString()));
                GameObject.Find("consonantTopicPercent4").GetComponent<Text>().text = String.Format("{0}%", percent);

                listResult = DatabaseConnection.getExecuteReader(String.Format(sqlPercentErrorUnit, userId, "111"));
                percent = getPercent(Double.Parse(listResult[0]["allExam"].ToString()), Double.Parse(listResult[0]["correctExam"].ToString()));
                GameObject.Find("consonantTopicPercent5").GetComponent<Text>().text = String.Format("{0}%", percent);
                
                // get most exam error (3 units)
                listResult = DatabaseConnection.getExecuteReader(String.Format(sqlErrorUnit, userId, "110"));
                string errorUnits = "";
                errorUnits = getErrorUnits(listResult);
                GameObject.Find("consonantTopicResult4").GetComponent<Text>().text = String.Format("{0}", errorUnits);

                listResult = DatabaseConnection.getExecuteReader(String.Format(sqlErrorUnit, userId, "111"));
                errorUnits = getErrorUnits(listResult); errorUnits = getErrorUnits(listResult);
                GameObject.Find("consonantTopicResult5").GetComponent<Text>().text = String.Format("{0}", errorUnits);

                break;
            case 200:
                break;
            case 300:
                break;
            case 400:
                break;
            case 500:
                break;
        }
    }

    private static string getMapConsonantNo(int no)
    {
        return consonantList[no - 1];
    }

    private static string getErrorUnits(List<Dictionary<String, System.Object>> listResult)
    {
        string errorUnits = "";
        foreach (Dictionary<String, System.Object> map in listResult)
        {
            if (!errorUnits.Equals(""))
                errorUnits += String.Format(", {0}", getMapConsonantNo(Int32.Parse(map["errorUnit"].ToString())));
            else
                errorUnits += String.Format("{0}", getMapConsonantNo(Int32.Parse(map["errorUnit"].ToString())));
        }

        return errorUnits;
    }

    private static string getPercent(double divisor, double set)
    {
        return ((set * 100) / divisor).ToString("0.00");
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
