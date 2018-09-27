using System;

public class BaseUnitLevelEntity : BaseLevelEntity
{

    public BaseUnitLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage, GameEntity gameEntity) 
        : base(baseMaxHealth, baseMovementSpeed, attackSpeed, baseDamage, gameEntity)
    {
    }

    public override EntityType Entity
    {
        get { return EntityType.Player; }
    }
}