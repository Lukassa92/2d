public class Unit : BaseEntity
{
    public int UnitId { get; set; }

    public int ElementId { get; set; }

    public int CombatClassId { get; set; }

    public int Strength { get; set; }

    public int Agility { get; set; }

    public int Speed { get; set; }

    public int Health { get; set; }

    public int Mana { get; set; }

}
