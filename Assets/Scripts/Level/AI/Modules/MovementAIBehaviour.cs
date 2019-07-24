using System;
using System.Collections.Generic;
using System.Linq;
using Level.Classes;
using UnityEngine;

namespace Level.AI
{
    public class MovementAIBehaviour : BaseAIBehaviour
    {
        public MovementAIBehaviour(GameEntity owner) : base(owner)
        {
            ActionPriority = 80;
        }

        private IEnumerable<GameEntity> GetRelevantTargets()
        {
            return EntitiesInViewRange.Where(e => e.EntityType != Owner.EntityType && e.EntityType != EntityType.Obstacle);
        }

        internal override TimeSpan Execute()
        {
            var relevantTargets = GetRelevantTargets().ToList();
            Debug.Log("Relevant Target count: " + relevantTargets.Count);
            if (EntitiesInViewRange.Count > 0 && relevantTargets.Count != 0)
            {
                var closest = GetClosestTarget(relevantTargets);
                Owner.Store.Dispatch(new MoveToLocationAction(closest.transform.position));
            }
            return TimeSpan.FromSeconds(0.25);
        }
    }
}