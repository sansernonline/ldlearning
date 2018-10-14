using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using SQLite4Unity3d;
using System.Data;
using System;
using System.IO;
using System.Linq;

public class DatabaseConnection : MonoBehaviour
{
    private static string getConnectionString()
    {
        string DatabaseName = "LD";

#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
            var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
            // then save to Application.persistentDataPath
            File.Copy(loadDb, filepath);
#elif UNITY_WP8
            var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
            // then save to Application.persistentDataPath
            File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		    var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		    // then save to Application.persistentDataPath
		    File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		    var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		    // then save to Application.persistentDataPath
		    File.Copy(loadDb, filepath);
#else
	        var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	        // then save to Application.persistentDataPath
	        File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif

        return dbPath;
    }
    
    /*
    public static int getMaxId(string tableName)
    {
        string sql = String.Format("SELECT Id From {0} ORDER BY Id DESC LIMIT 1 ", tableName);
        List<Dictionary<string, System.Object>> listResult = getExecuteReader(sql);
        int maxId = 0;

        if (listResult.Count > 0)
            maxId = Convert.ToInt32(listResult[0]["Id"].ToString());

        return maxId + 1;
    }*/

    public static User findUser()
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            return dbConn.Table<User>().FirstOrDefault();
        }
    }

    public static User findUser(string userId)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            return dbConn.Table<User>().Where(x => x.Id == userId).FirstOrDefault();
        }
    }


    public static void insertUser(User u)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            dbConn.Insert(u);
        }
    }

    public static void updateUser(User u)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            dbConn.Update(u);
        }
    }

    public static void insertUserCourseScore(UserCourseScore ucs)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            dbConn.Insert(ucs);
        }
    }

    public static void insertExamScore(ExamScore es)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            dbConn.Insert(es);
        }
    }

    public static int getLatestLesson(string userId, int courseDetailId)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            return dbConn.Table<UserCourseScore>().Where(x => x.UserId == userId && x.CourseDetailId == courseDetailId).Max(x => x.LearningUnit);
        }
    }

    public static int getCountLesson(string userId, int courseDetailId)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            return dbConn.Table<UserCourseScore>().Where(x => x.UserId == userId && x.CourseDetailId == courseDetailId).GroupBy(x => x.LearningUnit).Count();
        }
    }

    public static double getLearningTime(string userId, int courseDetailId)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            return dbConn.Table<UserCourseScore>().Where(x => x.UserId == userId && x.CourseDetailId == courseDetailId).Sum(x => x.LearningTime);
        }
    }

    public static double getExamAllScore(string userId, int examId)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            return dbConn.Table<ExamScore>().Where(x => x.UserId == userId && x.ExamId == examId).Count();
        }
    }

    public static double getExamCorrectScore(string userId, int examId)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {
            return dbConn.Table<ExamScore>().Where(x => x.UserId == userId && x.ExamId == examId).Sum(x => x.MyScore);
        }
    }

    public static List<ExamScore> getErrorUnits(string userId, int examId)
    {
        string connectionString = getConnectionString();
        using (SQLiteConnection dbConn = new SQLiteConnection(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create))
        {

            var rows = dbConn.Table<ExamScore>()
            .Where(x => x.UserId == userId && x.ExamId == examId)
            .GroupBy(n => n.LearningUnit)
            .Select(n => new ExamScore
            {
                LearningUnit = n.Key,
                MyScore = n.Count()
            })
            .OrderByDescending(n => n.MyScore)
            .Take(3).ToList();
           
            return rows;
        }
    }
}
