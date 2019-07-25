using Classes;

namespace Level.Classes
{
    public class LevelEntity
    {
        private int _health;
        private int _baseMaxHealth;

        protected LevelEntity(int baseMaxHealth, float baseMovementSpeed, float attackSpeed, int baseDamage, EntityType entityType, GameEntity gameEntity)
        {
            GameEntity = gameEntity;
            _baseMaxHealth = baseMaxHealth;
            _health = baseMaxHealth;
            BaseMovementSpeed = baseMovementSpeed;
            AttackSpeed = attackSpeed;
            BaseDamage = baseDamage;
            EntityType = entityType;
        }

        public LevelEntity(ILevelEntityStats settings, GameEntity gameEntity) : this(settings.MaxHealth, settings.BaseMovementSpeed, settings.AttackSpeed, settings.BaseDamage, settings.EntityType, gameEntity)
        {
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

        public float AttackSpeed { get; set; }
        public int BaseDamage { get; set; }

        public int PhysicalResistance { get; set; }
        public int FireResistance { get; set; }
        public int ColdResistance { get; set; }
        public int EnergyResistance { get; set; }
        public int PoisonResistance { get; set; }

        public bool IsAlive { get; private set; } = true;

        public bool CanWalk { get; set; }
        public bool IsVisible { get; set; }

        public GameEntity GameEntity { get; }
        public EntityType EntityType { get; }

        public void DoDamage(DamageSource damageSource)
        {
            if (!OnBeforeDamaged(damageSource))
            {
                damageSource.DamageDealt = false;
                return;
            }
            var healthAfterDamage = Health - damageSource.Damage;
            if (healthAfterDamage > 0)
            {
                Health -= damageSource.Damage;
                damageSource.DamageDealt = true;
                OnAfterDamaged(damageSource, healthAfterDamage);
            }
            else if (Kill(damageSource))
            {
                damageSource.TargetKilled = true;
            }
        }


        public virtual bool OnBeforeDamaged(DamageSource damageSource)
        {
            return true;
        }

        public virtual void OnAfterDamaged(DamageSource damageSource, int healthAfterDamage)
        {
            var action = new HealthChangedAction(healthAfterDamage, _baseMaxHealth);
            GameEntity.Store.Dispatch(action);
        }

        public bool Kill(DamageSource damageSource)
        {
            if (!OnBeforeDeath(damageSource))
                return false;

            OnDeath();
            IsAlive = false;
            Health = 0;
            OnAfterDeath(damageSource);
            return true;
        }

        public virtual bool OnBeforeDeath(DamageSource damageSource)
        {
            return true;
        }
        public virtual void OnDeath() { }

        public virtual void OnAfterDeath(DamageSource damageSource)
        {
            GameEntity.Store.Dispatch(new EntityDeathAction(damageSource));
        }
    }
}