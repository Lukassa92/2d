using System;

public class MeleeEnemyLevelEntity : BaseEnemyLevelEntity
{
    public MeleeEnemyLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage)
        : base(baseMaxHealth, baseMovementSpeed, attackSpeed, baseDamage)
    {
    }

    public MeleeEnemyLevelEntity() : base(100, MovementSpeed.Normal, TimeSpan.FromSeconds(1), 5)
    {

    }
}