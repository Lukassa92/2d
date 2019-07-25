using Level.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Level.AI
{
    public class MovementAIBehaviour : BaseAIBehaviour
    {
        private IEnumerable<GameEntity> GetRelevantTargets()
        {
            return EntitiesInViewRange.Where(e => e.IsAlive && e.EntityType != Owner.EntityType && e.EntityType != EntityType.Obstacle);
        }

        internal override TimeSpan Execute()
        {
            base.Execute();

            var relevantTargets = GetRelevantTargets().ToList();
            if (GetRelevantTargets().Any())
            {
                var closest = GetClosestTarget(relevantTargets);
                Owner.Store.Dispatch(new MoveToEntityAction(closest));
            }
            return TimeSpan.FromSeconds(0.09);
        }

        public override void OnTick()
        {
            ActionPriority = GetRelevantTargets().Any() ? 80 : 0;
        }
    }
}