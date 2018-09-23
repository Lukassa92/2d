
using System;
using System.Collections.Generic;

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

        public override void OnEntityEnteredViewRadius(LevelEntity entity)
        {
            _behaviours.ForEach(b => b.OnEntityEnteredViewRadius(entity));
        }

        public override void OnEntityLeftViewRadius(LevelEntity entity)
        {
            _behaviours.ForEach(b => b.OnEntityLeftViewRadius(entity));
        }

        public override void OnEntityEnteredAttackRadius(LevelEntity entity)
        {
            _behaviours.ForEach(b => b.OnEntityEnteredAttackRadius(entity));
        }

        public override void OnEntityLeftAttackRadius(LevelEntity entity)
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

        public override void OnCollisionWith(LevelEntity entity)
        {
            _behaviours.ForEach(b => b.OnCollisionWith(entity));
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

public class MovementBehaviour : BaseAIBehaviour
{
    private readonly CharacterMovement _movement;

    public MovementBehaviour(CharacterMovement movement)
    {
        _movement = movement;
    }

    public override void OnEntityEnteredViewRadius(LevelEntity entity)
    {
        base.OnEntityEnteredViewRadius(entity);
    }

    internal override TimeSpan ActionOffset
    {
        get { return TimeSpan.FromMilliseconds(100); }
    }
}

public class AIEventReceiver
{
    public virtual void OnEntityEnteredViewRadius(LevelEntity entity) { }
    public virtual void OnEntityLeftViewRadius(LevelEntity entity) { }
    public virtual void OnEntityEnteredAttackRadius(LevelEntity entity) { }
    public virtual void OnEntityLeftAttackRadius(LevelEntity entity) { }
    public virtual void OnOwnerDamaged(DamageSource source) { }
    public virtual void OnOwnerSpawned() { }
    public virtual void OnOwnerHealed(HealSource source) { }
    public virtual void OnDamagedOther(DamageSource source) { }
    public virtual void OnCollisionWith(LevelEntity entity) { }
}