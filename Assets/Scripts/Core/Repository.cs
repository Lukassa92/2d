using System.Collections.Generic;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    public T GetByID(int id)
    {
        throw new System.NotImplementedException();
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