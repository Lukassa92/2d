using SimpleSQL;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class DatabaseService : MonoBehaviour
    {
        public TextAsset DatabaseAsset;
        public SimpleSQLManager SqlManager;
        private bool _initialized = false;

        public void Start()
        {
            Initialize();
        }


        public void Initialize()
        {
            if (_initialized)
                return;
            var manager = new SimpleSQLManager
            {
                databaseFile = DatabaseAsset
            };
            SqlManager = manager;
        }

        public Repository<T> GetRepository<T>() where T : BaseEntity, new()
        {
            return new Repository<T>(this);
        }
    }
}
