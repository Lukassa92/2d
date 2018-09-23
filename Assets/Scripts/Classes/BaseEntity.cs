using SimpleSQL;

public class BaseEntity
{
    public BaseEntity()
    {
    }

    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
}