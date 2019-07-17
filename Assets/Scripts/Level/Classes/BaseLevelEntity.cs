﻿using Level.Classes;
using System;

public abstract partial class BaseLevelEntity : InteractableEntity
{
    private int _health;
    private int _baseMaxHealth;
    private bool _isAlive = true;
    private float _baseMovementSpeed = MovementSpeed.Normal;
    private TimeSpan _attackSpeed = TimeSpan.FromSeconds(1);
    private int _baseDamage = 5;
    private readonly GameEntity _gameEntity;

    protected BaseLevelEntity()
    {
    }

    protected BaseLevelEntity(int baseMaxHealth, float baseMovementSpeed, TimeSpan attackSpeed, int baseDamage, GameEntity gameEntity)
    {
        _gameEntity = gameEntity;
        _baseMaxHealth = baseMaxHealth;
        _health = baseMaxHealth;
        _baseMovementSpeed = baseMovementSpeed;
        _attackSpeed = attackSpeed;
        _baseDamage = baseDamage;
    }

    public float BaseMovementSpeed
    {
        get { return _baseMovementSpeed; }
        set { _baseMovementSpeed = value; }
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

    public float HealthPercentage
    {
        get
        {
            if (!IsAlive || Health == 0)
                return 0;

            return Health / BaseMaxHealth;
        }
    }

    public TimeSpan AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }

    public int BaseDamage
    {
        get { return _baseDamage; }
        set { _baseDamage = value; }
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

        var action = new HealthChangedAction(Health, healthAfterDamage, _baseMaxHealth);
        _gameEntity.Store.Dispatch(action);

        if (healthAfterDamage > 0 || !Kill(damageSource))
        {
            Health -= damageSource.Damage;
            OnAfterDamaged(damageSource);
        }
    }

    public GameEntity GameEntity
    {
        get { return _gameEntity; }
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
            IsAlive = false;
            Health = 0;
            OnAfterDeath();
            return true;
        }
        return false;
    }

    public virtual bool OnBeforeDeath(DamageSource damageSource)
    {
        return true;
    }
    public virtual void OnDeath() { }
    public virtual void OnAfterDeath() { }
}