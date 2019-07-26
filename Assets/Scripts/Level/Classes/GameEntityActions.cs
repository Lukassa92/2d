using Core;
using UnityEngine;

namespace Level.Classes
{
    public enum GameEntityActionTypes
    {
        HealthChanged,
        MoveToDirection,
        MeleeAttackAction,
        LookAt,
        StopMovement,
        DamagedBy,
        DamagedDealtTo,
        MoveToEntity,
        EntityDeath,
        RangedAttackAction
    }

    public class HealthChangedAction : GameAction<GameEntityActionTypes>
    {
        public int NewHealth { get; }
        public int MaxHealth { get; }

        public HealthChangedAction(int newHealth, int maxHealth) : base(GameEntityActionTypes.HealthChanged)
        {
            NewHealth = newHealth;
            MaxHealth = maxHealth;
        }
    }

    public class MoveToLocationAction : GameAction<GameEntityActionTypes>
    {
        public Vector3 TargetPosition { get; }

        public MoveToLocationAction(Vector3 targetPosition) : base(GameEntityActionTypes.MoveToDirection)
        {
            TargetPosition = targetPosition;
        }
    }

    public class MoveToEntityAction : GameAction<GameEntityActionTypes>
    {
        public GameObject TargetGameObject { get; set; }

        public MoveToEntityAction(GameEntity entity) : base(GameEntityActionTypes.MoveToEntity)
        {
            TargetGameObject = entity.gameObject;
        }
    }

    public class StopMovementAction : GameAction<GameEntityActionTypes>
    {
        public StopMovementAction() : base(GameEntityActionTypes.StopMovement)
        {
        }
    }

    public class LookAtAction : GameAction<GameEntityActionTypes>
    {
        public Vector3 TargetPosition { get; }

        public LookAtAction(Vector3 targetPosition) : base(GameEntityActionTypes.LookAt)
        {
            TargetPosition = targetPosition;
        }
    }

    public class MeleeAttackTargetAction : GameAction<GameEntityActionTypes>
    {
        public GameEntity AttackTarget { get; }

        public MeleeAttackTargetAction(GameEntity attackTarget) : base(GameEntityActionTypes.MeleeAttackAction)
        {
            AttackTarget = attackTarget;
        }
    }

    public class DamagedByAction : GameAction<GameEntityActionTypes>
    {
        public DamageSource DamageSource { get; }

        public DamagedByAction(DamageSource damageSource) : base(GameEntityActionTypes.DamagedBy)
        {
            DamageSource = damageSource;
        }
    }

    public class DamageDealtToAction : GameAction<GameEntityActionTypes>
    {
        public DamageSource DamageSource { get; }

        public DamageDealtToAction(DamageSource damageSource) : base(GameEntityActionTypes.DamagedDealtTo)
        {
            DamageSource = damageSource;
        }
    }

    public class EntityDeathAction : GameAction<GameEntityActionTypes>
    {
        public DamageSource DamageSource { get; }

        public EntityDeathAction(DamageSource damageSource) : base(GameEntityActionTypes.EntityDeath)
        {
            DamageSource = damageSource;
        }

    }

    public class RangedAttackAction : GameAction<GameEntityActionTypes>
    {
        public GameEntity Target { get; }
        public Rigidbody2D ProjectilePrefab { get; }
        public float AirborneTime { get; }

        public RangedAttackAction(GameEntity target, Rigidbody2D projectilePrefab, float airborneTime) : base(GameEntityActionTypes.RangedAttackAction)
        {
            Target = target;
            ProjectilePrefab = projectilePrefab;
            AirborneTime = airborneTime;
        }
    }
}
