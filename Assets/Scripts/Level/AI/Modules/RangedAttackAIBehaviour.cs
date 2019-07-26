using Level.Classes;
using System;
using System.Linq;
using UnityEngine;

namespace Level.AI
{
    public class RangedAttackAIBehaviour : BaseAIBehaviour
    {
        public Rigidbody2D ProjectilePrefab;
        [Range(0, float.MaxValue)]
        public float airborneTime = 1.0f;
        private GameEntity _attackTarget;

        public override void OnEntityEnteredAttackRadius(GameEntity entity)
        {
            if ((_attackTarget == null || !_attackTarget.IsAlive) && IsValidAttackTarget(entity))
            {
                _attackTarget = entity;
            }
        }

        public override void OnEntityDied(GameEntity entity)
        {
            base.OnEntityDied(entity);

            if (entity == _attackTarget)
                EnemyDisappeared();
        }

        public override void OnEntityDestroyed(GameEntity entity)
        {
            base.OnEntityDestroyed(entity);

            if (entity == _attackTarget)
                EnemyDisappeared();
        }

        private void EnemyDisappeared()
        {
            ActionPriority = 0;
            _attackTarget = null;
        }

        public override void OnTick()
        {
            base.OnTick();
            if (_attackTarget != null && _attackTarget.IsAlive)
            {
                ActionPriority = 100;
                return;
            }

            if (!EntitiesInAttackRange.Any())
                return;

            var newTarget = EntitiesInAttackRange.Where(IsValidAttackTarget).FirstOrDefault();
            if (newTarget == null)
                return;

            ActionPriority = 100;
            _attackTarget = newTarget;
        }

        internal override TimeSpan Execute()
        {
            if (_attackTarget == null)
            {
                EnemyDisappeared();
                return TimeSpan.Zero;
            }

            Owner.Store.Dispatch(new RangedAttackAction(_attackTarget, ProjectilePrefab, airborneTime));

            return TimeSpan.FromSeconds(1.5);
        }

        private bool IsValidAttackTarget(GameEntity e)
        {
            return e.IsAlive && e.EntityType != Owner.EntityType && e.EntityType != EntityType.Obstacle;
        }
    }
}
