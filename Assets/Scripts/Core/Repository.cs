using Assets.Scripts.Services;
using System.Collections.Generic;
using System.Linq;

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
        _databaseService.SqlManager.Insert(entity);
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

