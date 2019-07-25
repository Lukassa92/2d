using Level.Classes;
using System;

namespace Level.AI
{
    public class MeleeAttackAIBehaviour : BaseAIBehaviour
    {
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

            EnemyDisappeared();
        }

        public override void OnEntityDestroyed(GameEntity entity)
        {
            base.OnEntityDestroyed(entity);

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
            }
        }

        internal override TimeSpan Execute()
        {
            if (_attackTarget == null)
            {
                return TimeSpan.MinValue;
            }

            Owner.Store.Dispatch(new MeleeAttackTargetAction(_attackTarget));

            return TimeSpan.FromSeconds(0.5);
        }

        private bool IsValidAttackTarget(GameEntity e)
        {
            return e.IsAlive && e.EntityType != Owner.EntityType && e.EntityType != EntityType.Obstacle;
        }
    }
}