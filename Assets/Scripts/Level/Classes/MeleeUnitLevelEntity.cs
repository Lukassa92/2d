using System;

public class MeleeUnitLevelEntity : BaseUnitLevelEntity
{
    public MeleeUnitLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage)
        : base(baseMaxHealth, baseMovementSpeed, attackSpeed, baseDamage)
    {
    }

    public MeleeUnitLevelEntity() : base(300, MovementSpeed.Normal, TimeSpan.FromSeconds(1), 5)
    {

    }
}