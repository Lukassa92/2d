using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
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
            InitializeApplicationDatabase();
        }

        public Repository<T> GetRepository<T>() where T : BaseEntity, new()
        {
            return new Repository<T>(this);
        }

        private void InitializeApplicationDatabase()
        {
            var factory = new RepositoryFactory(this);
            var repos = factory.GetAllRepositories();
            repos.ForEach(r => r.CreateTableIfNotExits());
        }

        internal class RepositoryFactory
        {
            private readonly DatabaseService _databaseService;

            public RepositoryFactory(DatabaseService databaseService)
            {
                _databaseService = databaseService;
            }

            public IRepository GetRepository(Type type)
            {
                var method = typeof(RepositoryFactory).GetMethods()
                    .First(m => m.IsGenericMethod && m.Name.Equals("GetRepository"));
                var genericMethod = method.MakeGenericMethod(type);
                var repository = (IRepository)genericMethod.Invoke(this, new object[] { });
                return repository;
            }

            [UsedImplicitly]
            public Repository<T> GetRepository<T>() where T : BaseEntity, new()
            {
                return new Repository<T>(_databaseService);
            }

            public List<IRepository> GetAllRepositories()
            {
                return GetAllModelTypes().Select(type => GetRepository(type)).ToList();
            }

            public static List<Type> GetAllModelTypes()
            {
                var type = typeof(BaseEntity);
                var types = type.Assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(type))
                    .ToList();
                return types;
            }
        }

    }
}
