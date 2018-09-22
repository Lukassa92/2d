using System.Collections.Generic;
using Assets.Scripts.Services;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DatabaseService _databaseService;

    public Repository(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    public T GetByID(int id)
    {
    }

    public IEnumerable<T> GetAll()
    {
        throw new System.NotImplementedException();
    }

    public void Create(T entity)
    {
        throw new System.NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new System.NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new System.NotImplementedException();
    }
}