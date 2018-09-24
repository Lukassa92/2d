using System;
using System.Collections.Generic;
using System.Linq;

public class MeleeMovementAIBehaviour : MovementAIBehaviour
{
    public MeleeMovementAIBehaviour(GameEntity owner) : base(owner)
    {
        ActionPriority = 80;
    }
    private IEnumerable<GameEntity> GetRelevantTargets()
    {
        Movement.DebugLog("Number of EntitiesinView: "+EntitiesInView.Count);
        return EntitiesInView.Where(e => e.EntityType != Owner.EntityType);
    }

    internal override TimeSpan Execute()
    {
        var relevantTargets = GetRelevantTargets().ToList();
        if (EntitiesInView.Count > 0 && relevantTargets.Count != 0)
        {
            var closest = GetClosestTarget(relevantTargets);
            Movement.LookAt(closest.Position);
            Movement.RunTo(closest.Position);
        }
        else
        {
            Movement.LookAt(Owner.EntityType == EntityType.Enemy
                ? States.MoveDirection.Left
                : States.MoveDirection.Right);
            Movement.RunTo(Owner.EntityType == EntityType.Enemy
                ? States.MoveDirection.Left
                : States.MoveDirection.Right);
        }

        return ActionOffset;
    }
}