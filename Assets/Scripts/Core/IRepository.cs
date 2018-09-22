using System.Collections.Generic;

public interface IRepository<T> where T : BaseEntity
{
    T GetByID(int id);
    IEnumerable<T> GetAll();
    void Create(T entity);
    void Delete(T entity);
    void Update(T entity);
}