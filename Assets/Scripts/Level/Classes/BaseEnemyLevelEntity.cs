using System;

public class BaseEnemyLevelEntity : LevelEntity
{
    public BaseEnemyLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage)
        : base(baseMaxHealth, baseMovementSpeed, attackSpeed, baseDamage)
    {
    }


    public override EntityType Entity
    {
        get { return EntityType.Enemy; }
    }
}