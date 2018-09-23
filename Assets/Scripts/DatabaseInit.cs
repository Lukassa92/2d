using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine;

public class DatabaseInit : MonoBehaviour
{
    private string databasePath;
    // Use this for initialization
    void Start()
    {
        databasePath = "URI=file:" + Application.persistentDataPath + "/2dGame.db";
        CreateSqLiteDatabase();
    }

    private void CreateSqLiteDatabase()
    {
        if (!File.Exists(databasePath))
        {
            Debug.Log("LS: path: " + databasePath + " for this platform: " + Application.platform);
            try
            {
                IDbConnection databaseConnection;
                databaseConnection = (IDbConnection)new SqliteConnection(databasePath);
                databaseConnection.Open();

                /** How to create a simple SQL Statement **/
                //IDbCommand databaseCommand = databaseConnection.CreateCommand();
                //databaseCommand.CommandText = "Create Table test(a, b, c)";
                //databaseCommand.ExecuteNonQuery();

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                Debug.Log("error: " + ex.Message);
            }
        }
        else
        {
            Debug.Log("LS: There is a database file.");
        }
    }
}
