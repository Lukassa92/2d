using System;

public class MeleeUnitLevelEntity : BaseUnitLevelEntity
{
    public MeleeUnitLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage, GameEntity gameEntity)
        : base(baseMaxHealth, baseMovementSpeed, attackSpeed, baseDamage, gameEntity)
    {
    }

    //public MeleeUnitLevelEntity() : base(300, MovementSpeed.Normal, TimeSpan.FromSeconds(1), 5)
    //{

    //}
}