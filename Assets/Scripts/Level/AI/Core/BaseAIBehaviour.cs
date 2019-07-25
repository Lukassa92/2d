using System;
using System.Collections.Generic;
using Level.Classes;
using MoreLinq;
using UnityEngine;

namespace Level.AI
{
    public abstract class BaseAIBehaviour : MonoBehaviour, IAIEventReceiver
    {
        internal List<GameEntity> EntitiesInViewRange { get; private set; }
        internal List<GameEntity> EntitiesInAttackRange { get; private set; }
        internal GameEntity Owner { get; private set; }

        private void Start()
        {
            EntitiesInViewRange = new List<GameEntity>();
            EntitiesInAttackRange = new List<GameEntity>();
            Owner = GetComponentInParent<GameEntity>();
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

        public virtual void OnEntityEnteredAttackRadius(GameEntity entity)
        {
            if (!EntitiesInAttackRange.Contains(entity))
                EntitiesInAttackRange.Add(entity);
        }

        public virtual void OnEntityLeftAttackRadius(GameEntity entity)
        {
            if(EntitiesInAttackRange.Contains(entity))
                EntitiesInAttackRange.Remove(entity);
        }

        public virtual void OnOwnerDamaged(DamageSource source) { }
        public virtual void OnOwnerSpawned() { }
        public virtual void OnOwnerDied() { }
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