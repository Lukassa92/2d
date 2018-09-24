﻿using System;

public class DamageService
{
    private static DamageService _damageService;
    public static DamageService GetService()
    {
        if (_damageService == null)
            _damageService = new DamageService();

        return _damageService;
    }

    private int RandomInt(int min, int max)
    {
        var random = new Random();
        return random.Next(min, max);
    }

    public void DoDamage(LevelEntity source, LevelEntity target, int minDamage, int maxDamage)
    {
        DoDamage(source, target, RandomInt(minDamage, maxDamage));
    }

    public void DoDamage(LevelEntity source, LevelEntity target, int damage)
    {
        var damageSource = new DamageSource
        {
            Source = source,
            Target = target,
            Damage = damage
        };
        target.DoDamage(damageSource);
    }

    public void DoDamage(LevelEntity target, int minDamage, int maxDamage)
    {
        DoDamage(target, RandomInt(minDamage, maxDamage));
    }

    public void DoDamage(LevelEntity target, int damage)
    {
        var damageSource = new DamageSource
        {
            Target = target,
            Damage = damage
        };
        target.DoDamage(damageSource);
    }
}