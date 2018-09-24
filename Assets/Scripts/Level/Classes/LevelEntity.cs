using System;
using System.Linq;
using Assets.Scripts.Level.Classes;

public abstract class LevelEntity : InteractableEntity
{
    private int _health;
    private bool _isAlive = true;
    private int _baseMaxHealth;
    private float _speed = MovementSpeed.Normal;

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public int BaseMaxHealth
    {
        get { return _baseMaxHealth; }
        set
        {
            var val = value < 1 ? 1 : value;
            _baseMaxHealth = val;
        }
    }

    public int Health
    {
        get { return IsAlive ? _health : 0; }
        set
        {
            var val = value;
            if (val > BaseMaxHealth)
            {
                val = BaseMaxHealth;
            }
            else if (val < 1)
            {
                val = 1;
            }
            _health = val;
        }
    }

    public int PhysicalResistance { get; set; }
    public int FireResistance { get; set; }
    public int ColdResistance { get; set; }
    public int EnergyResistance { get; set; }
    public int PoisonResistance { get; set; }

    public bool IsAlive
    {
        get { return _isAlive; }
        private set { _isAlive = value; }
    }

    public bool CanWalk { get; set; }
    public bool IsVisible { get; set; }

    public abstract EntityType Entity { get; }

    public void DoDamage(DamageSource damageSource)
    {
        if (!OnBeforeDamaged(damageSource))
        {
            return;
        }
        var healthAfterDamage = Health - damageSource.Damage;
        if (healthAfterDamage > 0 || !Kill(damageSource))
        {
            Health -= damageSource.Damage;
            OnAfterDamaged(damageSource);
        }
    }

    public virtual bool OnBeforeDamaged(DamageSource damageSource)
    {
        return true;
    }

    public virtual void OnAfterDamaged(DamageSource damageSource) { }

    public bool Kill(DamageSource damageSource)
    {
        if (OnBeforeDeath(damageSource))
        {
            OnDeath();
            Health = 0;
            IsAlive = false;
            OnAfterDeath();
            return false;
        }
        return true;
    }

    public virtual bool OnBeforeDeath(DamageSource damageSource)
    {
        return true;
    }
    public virtual void OnDeath() { }
    public virtual void OnAfterDeath() { }
}

public static class MovementSpeed
{
    public const float Slow = 75.0f;
    public const float Normal = 150.0f;
    public const float Fast = 225.0f;
}

public class BaseUnitLevelEntity : LevelEntity
{
    public override EntityType Entity
    {
        get { return EntityType.Player; }
    }
}

public class BaseEnemyLevelEntity : LevelEntity
{
    public override EntityType Entity
    {
        get { return EntityType.Enemy; }
    }
}

public class MeleeUnitLevelEntity : BaseUnitLevelEntity
{
    public MeleeUnitLevelEntity()
    {
        BaseMaxHealth = 100;
        Health = 100;
    }
}

public class MeleeEnemyLevelEntity : BaseEnemyLevelEntity
{
    public MeleeEnemyLevelEntity()
    {
        BaseMaxHealth = 100;
        Health = 100;
    }
}

public class LevelEntityFactory
{
    public static LevelEntity CreateLevelEntity(string name)
    {
        var type = typeof(LevelEntity).Assembly
            .GetTypes().FirstOrDefault(t => t.IsSubclassOf(typeof(LevelEntity)) && !t.IsAbstract && t.Name == name);
        if (type == null)
            throw new InvalidOperationException("No level entity class found with the name " + name);
        return (LevelEntity)Activator.CreateInstance(type);
    }
}