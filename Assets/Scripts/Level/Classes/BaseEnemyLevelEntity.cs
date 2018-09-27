using System;

public class BaseEnemyLevelEntity : BaseLevelEntity
{
    public BaseEnemyLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage, GameEntity gameEntity)
        : base(baseMaxHealth, baseMovementSpeed, attackSpeed, baseDamage, gameEntity)
    {
    }


    public override EntityType Entity
    {
        get { return EntityType.Enemy; }
    }
}