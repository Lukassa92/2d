using System;

public class BaseUnitLevelEntity : LevelEntity
{

    public BaseUnitLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage) 
        : base(baseMaxHealth, baseMovementSpeed, attackSpeed, baseDamage)
    {
    }

    public override EntityType Entity
    {
        get { return EntityType.Player; }
    }
}