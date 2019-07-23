using Level.Classes;
using System;

public abstract partial class BaseLevelEntity : InteractableEntity
{
    private int _health;
    private int _baseMaxHealth;

    protected BaseLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage, GameEntity gameEntity)
    {
        GameEntity = gameEntity;
        _baseMaxHealth = baseMaxHealth;
        _health = baseMaxHealth;
        BaseMovementSpeed = baseMovementSpeed;
        AttackSpeed = attackSpeed;
        BaseDamage = baseDamage;
    }

    public float BaseMovementSpeed { get; set; }

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

    public TimeSpan AttackSpeed { get; set; }
    public int BaseDamage { get; set; }

    public int PhysicalResistance { get; set; }
    public int FireResistance { get; set; }
    public int ColdResistance { get; set; }
    public int EnergyResistance { get; set; }
    public int PoisonResistance { get; set; }

    public bool IsAlive { get; private set; } = true;

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

        var action = new HealthChangedAction(Health, healthAfterDamage, _baseMaxHealth);
        GameEntity.Store.Dispatch(action);

        if (healthAfterDamage > 0 || !Kill(damageSource))
        {
            Health -= damageSource.Damage;
            OnAfterDamaged(damageSource);
        }
    }

    public GameEntity GameEntity { get; }

    public virtual bool OnBeforeDamaged(DamageSource damageSource)
    {
        return true;
    }

    public virtual void OnAfterDamaged(DamageSource damageSource) { }

    public bool Kill(DamageSource damageSource)
    {
        if (!OnBeforeDeath(damageSource))
            return false;

        OnDeath();
        IsAlive = false;
        Health = 0;
        OnAfterDeath();
        return true;
    }

    public virtual bool OnBeforeDeath(DamageSource damageSource)
    {
        return true;
    }
    public virtual void OnDeath() { }
    public virtual void OnAfterDeath() { }
}