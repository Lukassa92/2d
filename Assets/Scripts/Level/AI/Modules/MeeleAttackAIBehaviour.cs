using Level.Classes;
using System;

namespace Level.AI
{
    public class MeeleAttackAI : BaseAIBehaviour
    {
        private GameEntity _attackTarget;

        public MeeleAttackAI() : base()
        {
        }

        public override void OnEntityEnteredAttackRadius(GameEntity entity)
        {
            base.OnEntityEnteredViewRadius(entity);

            _attackTarget = entity;
            ActionPriority = 100;
        }

        public override void OnEntityLeftAttackRadius(GameEntity entity)
        {
            base.OnEntityLeftViewRadius(entity);

            EnemyDisappeared();
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

        internal override TimeSpan Execute()
        {
            if (_attackTarget == null)
            {
                return TimeSpan.MinValue;
            }

            Owner.Store.Dispatch(new MeleeAttackTargetAction(_attackTarget));

            return TimeSpan.FromSeconds(0.5);
        }
    }
}