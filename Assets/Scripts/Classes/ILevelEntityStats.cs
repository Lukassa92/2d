namespace Classes
{
    public interface ILevelEntityStats
    {
        int Health { get; set; }
        int MaxHealth { get; set; }
        bool IsAlive { get; set; }
        EntityType EntityType { get; set; }
        float BaseMovementSpeed { get; set; }
        float AttackSpeed { get; set; }
        int BaseDamage { get; set; }
        float ViewRange { get; set; }
        float AttackRange { get; set; }
    }
}
