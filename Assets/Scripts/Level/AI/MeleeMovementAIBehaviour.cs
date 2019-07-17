using System;
using System.Collections.Generic;
using System.Linq;
using Level.Classes;

namespace Level.AI
{
    public class MeleeMovementAIBehaviour : MovementAIBehaviour
    {
        public MeleeMovementAIBehaviour(GameEntity owner) : base(owner)
        {
            ActionPriority = 80;
        }

        private IEnumerable<GameEntity> GetRelevantTargets()
        {
            return EntitiesInView.Where(e => e.EntityType != Owner.EntityType && e.EntityType != EntityType.Obstacle);
        }

        internal override TimeSpan Execute()
        {
            var relevantTargets = GetRelevantTargets().ToList();
            if (EntitiesInView.Count > 0 && relevantTargets.Count != 0)
            {
                var closest = GetClosestTarget(relevantTargets);
                Owner.Store.Dispatch(new MoveToLocationAction(closest.Position));
            }
//            else
//            {
//                MovementBehaviour.LookAt(Owner.EntityType == EntityType.Enemy
//                    ? MoveDirection.Left
//                    : MoveDirection.Right);
//                MovementBehaviour.RunTo(Owner.EntityType == EntityType.Enemy
//                    ? MoveDirection.Left
//                    : MoveDirection.Right);
//            }

            return ActionOffset;
        }
    }
}