using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Services;
using SimpleSQL;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
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

    public T GetByID(int id)
    {
        return GetTableEnumerable().FirstOrDefault(e => e.ID == id);
    }

    public IEnumerable<T> GetAll()
    {
        return GetTableEnumerable().ToList();
    }

    public void Create(T entity)
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

    public void Delete(T entity)
    {
        _databaseService.SqlManager.Delete(entity);
    }

    public void Update(T entity)
    {
        _databaseService.SqlManager.UpdateTable(entity);
    }
}