using MoreLinq;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Level.Classes
{
    public abstract class BaseAI : AIEventReceiver
    {
        public TargetEntity Owner { get; private set; }
        public CharacterMovement Movement { get; private set; }
        protected abstract List<BaseAIBehaviour> Behaviours { get; }

        private BaseAIBehaviour _lastExecutedBehaviour;
        private DateTime _nextExecutionDate = DateTime.Now;

        protected BaseAI(TargetEntity owner, CharacterMovement movement)
        {
            Owner = owner;
            Movement = movement;
        }

        private BaseAIBehaviour GetBehaviourWithHighestPriority()
        {
            return Behaviours.MaxBy(b => b.ActionPriority);
        }

        private void CheckNextExecution()
        {
            if (DateTime.Now >= _nextExecutionDate)
            {
                ExecuteNextBehaviour();
            }
        }

        private void ExecuteNextBehaviour()
        {
            var behaviour = GetBehaviourWithHighestPriority();
            ExecuteBehaviour(behaviour);
        }

        private void ExecuteBehaviour(BaseAIBehaviour behaviour)
        {
            if (behaviour != _lastExecutedBehaviour)
            {
                _lastExecutedBehaviour.Unselect(behaviour);
                _lastExecutedBehaviour = behaviour;
            }
            var delay = _lastExecutedBehaviour.Execute();
            _nextExecutionDate = DateTime.Now + delay;
        }

        public override void OnEntityEnteredViewRadius(TargetEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityEnteredViewRadius(entity));
        }

        public override void OnEntityLeftViewRadius(TargetEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityLeftViewRadius(entity));
        }

        public override void OnEntityEnteredAttackRadius(TargetEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityEnteredAttackRadius(entity));
        }

        public override void OnEntityLeftAttackRadius(TargetEntity entity)
        {
            Behaviours.ForEach(b => b.OnEntityLeftAttackRadius(entity));
        }

        public override void OnOwnerDamaged(DamageSource source)
        {
            Behaviours.ForEach(b => b.OnOwnerDamaged(source));
        }

        public override void OnOwnerSpawned()
        {
            Behaviours.ForEach(b => b.OnOwnerSpawned());
        }

        public override void OnOwnerHealed(HealSource source)
        {
            Behaviours.ForEach(b => b.OnOwnerHealed(source));
        }

        public override void OnDamagedOther(DamageSource source)
        {
            Behaviours.ForEach(b => b.OnDamagedOther(source));
        }

        public override void OnCollisionWith(TargetEntity entity)
        {
            Behaviours.ForEach(b => b.OnCollisionWith(entity));
        }

        public override void OnTick()
        {
            Behaviours.ForEach(b => b.OnTick());
            CheckNextExecution();
        }
    }
}