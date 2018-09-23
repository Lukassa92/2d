using SimpleSQL;

public class BaseEntity
{
    public BaseEntity()
    {
    }

    [PrimaryKey]
    public int ID { get; set; }
}