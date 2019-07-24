using System;
using System.Collections.Generic;
using MoreLinq;
using UnityEngine;

namespace Level.AI
{
    public abstract class BaseAIBehaviour : MonoBehaviour, IAIEventReceiver
    {
        internal readonly List<GameEntity> EntitiesInViewRange = new List<GameEntity>();
        internal readonly GameEntity Owner;

        protected BaseAIBehaviour(GameEntity owner)
        {
            Owner = owner;
        }


        [SerializeField]
        [Range(0, 100)]
        private int _actionPriority;
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

        public virtual void Unselect(BaseAIBehaviour behaviour)
        {
        }

        internal abstract TimeSpan Execute();

        public virtual void OnEntityEnteredAttackRadius(GameEntity entity) { }
        public virtual void OnEntityLeftAttackRadius(GameEntity entity) { }
        public virtual void OnOwnerDamaged(DamageSource source) { }
        public virtual void OnOwnerSpawned() { }
        public virtual void OnOwnerHealed(HealSource source) { }
        public virtual void OnDamagedOther(DamageSource source) { }
        public virtual void OnCollisionWith(GameEntity entity) { }
        public virtual void OnTick() { }

        public virtual void OnEntityEnteredViewRadius(GameEntity entity)
        {
            if (!EntitiesInViewRange.Contains(entity))
                EntitiesInViewRange.Add(entity);
        }

        public virtual void OnEntityLeftViewRadius(GameEntity entity)
        {
            if (EntitiesInViewRange.Contains(entity))
                EntitiesInViewRange.Remove(entity);
        }

        public virtual void OnEntityDied(GameEntity entity)
        {
            if (EntitiesInViewRange.Contains(entity))
                EntitiesInViewRange.Remove(entity);
        }

        public virtual void OnEntityDestroyed(GameEntity entity)
        {
            if (EntitiesInViewRange.Contains(entity))
                EntitiesInViewRange.Remove(entity);
        }

        public GameEntity GetClosestTarget(IEnumerable<GameEntity> targets)
        {
            return targets.MinBy(GetDistanceToTarget);
        }

        private float GetDistanceToTarget(GameEntity target)
        {
            return Vector3.Distance(Owner.transform.position, target.transform.position);
        }
    }
}