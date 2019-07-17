﻿using System;

public class DamageService
{
    private static DamageService _damageService;
    public static DamageService GetService()
    {
        return _damageService ?? (_damageService = new DamageService());
    }

    private int RandomInt(int min, int max)
    {
        var random = new Random();
        return random.Next(min, max);
    }

    public DamageSource DoDamage(BaseLevelEntity source, BaseLevelEntity target, int minDamage, int maxDamage)
    {
        return DoDamage(source, target, RandomInt(minDamage, maxDamage));
    }

    public DamageSource DoDamage(BaseLevelEntity source, BaseLevelEntity target, int damage)
    {
        var damageSource = new DamageSource
        {
            Source = source,
            Target = target,
            Damage = damage
        };
        target.DoDamage(damageSource);
        return damageSource;
    }

    public DamageSource DoDamage(BaseLevelEntity target, int minDamage, int maxDamage)
    {
        return DoDamage(target, RandomInt(minDamage, maxDamage));
    }

    public DamageSource DoDamage(BaseLevelEntity target, int damage)
    {
        var damageSource = new DamageSource
        {
            Target = target,
            Damage = damage
        };
        target.DoDamage(damageSource);
        return damageSource;
    }
}
