using Level.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Level.AI
{
    public class MovementAIBehaviour : BaseAIBehaviour
    {
        private IEnumerable<GameEntity> GetRelevantTargets()
        {
            return EntitiesInViewRange.Where(e => e.EntityType != Owner.EntityType && e.EntityType != EntityType.Obstacle);
        }

        internal override TimeSpan Execute()
        {
            var relevantTargets = GetRelevantTargets().ToList();
            if (EntitiesInViewRange.Count > 0 && relevantTargets.Count != 0)
            {
                Debug.Log("Relevant Target count: " + relevantTargets.Count);
                var closest = GetClosestTarget(relevantTargets);
                Owner.Store.Dispatch(new MoveToEntityAction(closest));
            }
            return TimeSpan.FromSeconds(0.09);
        }
    }
}