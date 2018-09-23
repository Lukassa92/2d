using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Services;
using SimpleSQL;

public class Repository<T> : IRepository where T : BaseEntity, new()
{
    private readonly DatabaseService _databaseService;

    public Repository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    private SimpleSQL.TableQuery<T> GetTableEnumerable()
    {
        return _databaseService.SqlManager.Table<T>();
    }

    public BaseEntity GetByID(int id)
    {
        return GetTableEnumerable().FirstOrDefault(e => e.ID == id);
    }

    public IEnumerable<BaseEntity> GetAll()
    {
        return GetTableEnumerable().AsEnumerable().Cast<BaseEntity>();
    }

    public void Create(BaseEntity entity)
    {
        try
        {
            var id = _databaseService.SqlManager.Insert(entity);
        }
        catch (SQLiteException e)
        {
            Console.WriteLine(e.GetType());
            throw;
        }
    }

    public void Delete(BaseEntity entity)
    {
        _databaseService.SqlManager.Delete(entity);
    }

    public void Update(BaseEntity entity)
    {
        _databaseService.SqlManager.UpdateTable(entity);
    }

    public void CreateTableIfNotExits()
    {
        _databaseService.SqlManager.CreateTable<T>();
    }
}

