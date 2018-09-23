
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Level.Classes
{
    public abstract class BaseAI : AIEventReceiver
    {
        private readonly List<BaseAIBehaviour> _behaviours;

        protected BaseAI()
        {
            _behaviours = new List<BaseAIBehaviour>();
        }

        protected BaseAI(params BaseAIBehaviour[] behaviours)
        {
            _behaviours = new List<BaseAIBehaviour>(behaviours);
        }

        public override void OnEntityEnteredViewRadius(TargetEntity entity)
        {
            _behaviours.ForEach(b => b.OnEntityEnteredViewRadius(entity));
        }

        public override void OnEntityLeftViewRadius(TargetEntity entity)
        {
            _behaviours.ForEach(b => b.OnEntityLeftViewRadius(entity));
        }

        public override void OnEntityEnteredAttackRadius(TargetEntity entity)
        {
            _behaviours.ForEach(b => b.OnEntityEnteredAttackRadius(entity));
        }

        public override void OnEntityLeftAttackRadius(TargetEntity entity)
        {
            _behaviours.ForEach(b => b.OnEntityLeftAttackRadius(entity));
        }

        public override void OnOwnerDamaged(DamageSource source)
        {
            _behaviours.ForEach(b => b.OnOwnerDamaged(source));
        }

        public override void OnOwnerSpawned()
        {
            _behaviours.ForEach(b => b.OnOwnerSpawned());
        }

        public override void OnOwnerHealed(HealSource source)
        {
            _behaviours.ForEach(b => b.OnOwnerHealed(source));
        }

        public override void OnDamagedOther(DamageSource source)
        {
            _behaviours.ForEach(b => b.OnDamagedOther(source));
        }

        public override void OnCollisionWith(TargetEntity entity)
        {
            _behaviours.ForEach(b => b.OnCollisionWith(entity));
        }

        public override void OnTick()
        {
            _behaviours.ForEach(b => b.OnTick());
        }
    }
}

public abstract class BaseAIBehaviour : AIEventReceiver
{
    public int ActionPriority
    {
        get { return _actionPriority; }
        set
        {
            var val = value;
            if (val < 0)
            {
                val = 0;
            }
            else if (val > 100)
            {
                val = 100;
            }
            _actionPriority = val;
        }
    }

    internal abstract TimeSpan ActionOffset { get; }
    internal DateTime LastAction = DateTime.MinValue;
    private int _actionPriority;

    internal bool NextActionPossible()
    {
        return DateTime.Now >= LastAction + ActionOffset;
    }
}

public abstract class MovementAIBehaviour : BaseAIBehaviour
{
    internal readonly CharacterMovement Movement;
    internal readonly TargetEntity Owner;
    internal readonly List<TargetEntity> EntitiesInView = new List<TargetEntity>();

    protected MovementAIBehaviour(CharacterMovement movement, TargetEntity owner)
    {
        Movement = movement;
        Owner = owner;
        // TODO: Nachher wieder entfernen
        ActionPriority = 100;
    }

    public override void OnEntityEnteredViewRadius(TargetEntity entity)
    {
        if (!EntitiesInView.Contains(entity))
            EntitiesInView.Add(entity);
    }

    public override void OnEntityLeftViewRadius(TargetEntity entity)
    {
        if (EntitiesInView.Contains(entity))
            EntitiesInView.Remove(entity);
    }

    public TargetEntity GetClosestTarget(IEnumerable<TargetEntity> targets)
    {
        return targets.MinBy(GetDistanceToTarget);
    }

    private float GetDistanceToTarget(TargetEntity target)
    {
        return Vector3.Distance(Owner.GameEntity.Position, target.GameEntity.Position);
    }

    internal override TimeSpan ActionOffset
    {
        get { return TimeSpan.FromMilliseconds(100); }
    }
}

public class MeleeMovementAIBehaviour : MovementAIBehaviour
{
    public MeleeMovementAIBehaviour(CharacterMovement movement, TargetEntity owner) : base(movement, owner)
    {
    }

    private IEnumerable<TargetEntity> GetRelevantTargets()
    {
        return EntitiesInView.Where(e => e.GameEntity.EntityType != Owner.GameEntity.EntityType);
    }

    public override void OnTick()
    {
        if (NextActionPossible())
        {
            var relevantTargets = GetRelevantTargets().ToList();
            if (EntitiesInView.Count > 0 && relevantTargets.Count == 0)
            {
                var closest = GetClosestTarget(relevantTargets);
                Movement.RunTo(closest.GameEntity.Position);
            }
            else
            {
                Movement.RunTo(Owner.GameEntity.EntityType == EntityType.Enemy ? States.MoveDirection.Left : States.MoveDirection.Right);
            }
        }
    }
}

public class AIEventReceiver
{
    public virtual void OnEntityEnteredViewRadius(TargetEntity entity) { }
    public virtual void OnEntityLeftViewRadius(TargetEntity entity) { }
    public virtual void OnEntityEnteredAttackRadius(TargetEntity entity) { }
    public virtual void OnEntityLeftAttackRadius(TargetEntity entity) { }
    public virtual void OnOwnerDamaged(DamageSource source) { }
    public virtual void OnOwnerSpawned() { }
    public virtual void OnOwnerHealed(HealSource source) { }
    public virtual void OnDamagedOther(DamageSource source) { }
    public virtual void OnCollisionWith(TargetEntity entity) { }
    public virtual void OnTick() { }
}