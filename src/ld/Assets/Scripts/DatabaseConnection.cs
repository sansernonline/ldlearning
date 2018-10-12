using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseConnection : MonoBehaviour {

    private static string db = "/Plugins/LD";

    public static void getExecuteScalar(string sql)
    {
        using (IDbConnection dbConn = new SqliteConnection("URI=file:" + Application.dataPath + db))
        {
            dbConn.Open();
            IDbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandText = sql;
            dbCmd.ExecuteScalar();

            dbCmd.Dispose();
            dbCmd = null;
            dbConn.Close();
        }
    }
    
    public static List<Dictionary<string, System.Object>> getExecuteReader(string sql)
    {
        List<Dictionary<string, System.Object>> listResult = new List<Dictionary<string, System.Object>>();

        using (IDbConnection dbConn = new SqliteConnection("URI=file:" + Application.dataPath + db))
        {
            dbConn.Open();
            IDbCommand dbCmd = dbConn.CreateCommand();
            dbCmd.CommandText = sql;
            IDataReader reader = dbCmd.ExecuteReader();

            // add to List<Mapp<T,T>>
            Dictionary<string, System.Object> map = null;

            while (reader.Read())
            {
                map = new Dictionary<string, System.Object>();
                
                for (int index = 0; index < reader.FieldCount; index++)
                {
                    System.Object val = null;
                    string fieldType = reader.GetFieldType(index).FullName;
                    
                    //Debug.Log("type : " + fieldType);
                    
                    if (fieldType.Equals("System.Int64"))
                    {
                        val = reader.GetInt64(index);
                        //Debug.Log("Int64 : " + reader.GetInt64(index));
                    }
                    else if (fieldType.Equals("System.String"))
                    {
                        val = reader.GetString(index);
                        //Debug.Log("String : " + reader.GetString(index));
                    }
                    map.Add(reader.GetName(index), val);
                }

                listResult.Add(map);
            }

            reader.Close();
            reader = null;
            dbCmd.Dispose();
            dbCmd = null;
            dbConn.Close();
        }

        return listResult;
    }

    public static int getMaxId(string tableName)
    {
        string sql = String.Format("SELECT Id From {0} ORDER BY Id DESC LIMIT 1 ", tableName);
        List<Dictionary<string, System.Object>> listResult = getExecuteReader(sql);
        int maxId = 0;

        if (listResult.Count > 0)
            maxId = Convert.ToInt32(listResult[0]["Id"].ToString());

        return maxId + 1;
    }
}
