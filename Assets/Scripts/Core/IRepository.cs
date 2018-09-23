using System.Collections.Generic;

public interface IRepository
{
    BaseEntity GetByID(int id);
    IEnumerable<BaseEntity> GetAll();
    void Create(BaseEntity entity);
    void Delete(BaseEntity entity);
    void Update(BaseEntity entity);
    void CreateTableIfNotExits();
}