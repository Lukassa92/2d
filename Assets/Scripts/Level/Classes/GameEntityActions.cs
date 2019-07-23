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
        DamagedDealtTo
    }

    public class HealthChangedAction : GameAction<GameEntityActionTypes>
    {
        public int OldHealth { get; }
        public int NewHealth { get; }
        public int MaxHealth { get; }

        public HealthChangedAction(int oldHealth, int newHealth, int maxHealth) : base(GameEntityActionTypes.HealthChanged)
        {
            OldHealth = oldHealth;
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

}
