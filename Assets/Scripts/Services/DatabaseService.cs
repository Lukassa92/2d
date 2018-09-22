using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class DatabaseService
    {
        private static readonly string DatabasePath = "URI=file:" + Application.persistentDataPath + "/2dGame.db";

        public void Initialize()
        {

        }

        private bool DatabaseExists()
        {
        }

        public Repository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(this);
        }

        public IDbConnection CreateDatabaseConnection()
        {
            return new SqliteConnection(DatabasePath);
        }
    }
}
