using System;

public class MeleeEnemyLevelEntity : BaseEnemyLevelEntity
{
    public MeleeEnemyLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage, GameEntity gameEntity)
        : base(baseMaxHealth, baseMovementSpeed, attackSpeed, baseDamage, gameEntity)
    {
    }

    //public MeleeEnemyLevelEntity() : base(100, BaseMovementSpeed.Normal, TimeSpan.FromSeconds(1), 5, new GameEntity())
    //{

    //}
}