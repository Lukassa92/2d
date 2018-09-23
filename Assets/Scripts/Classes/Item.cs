public class Item : BaseEntity
{

    public int ItemId { get; set; }

    public int ItemClassId { get; set; }

    public string Title { get; set; }

    public int Prize { get; set; }
}
