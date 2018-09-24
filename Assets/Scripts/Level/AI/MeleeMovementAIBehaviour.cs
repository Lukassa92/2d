using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class MeleeMovementAIBehaviour : MovementAIBehaviour
{
    public MeleeMovementAIBehaviour(CharacterMovement movement, TargetEntity owner) : base(movement, owner)
    {
    }
    private IEnumerable<TargetEntity> GetRelevantTargets()
    {
        Movement.DebugLog("Number of EntitiesinView: "+EntitiesInView.Count);
        return EntitiesInView.Where(e => e.GameEntity.EntityType != Owner.GameEntity.EntityType);
    }

    internal override TimeSpan Execute()
    {
        var relevantTargets = GetRelevantTargets().ToList();
//        Movement.DebugLog("relevantTargetCount: " + relevantTargets.Count);
        if (EntitiesInView.Count > 0 && relevantTargets.Count != 0)
        {
            var closest = GetClosestTarget(relevantTargets);
            Movement.LookAt(closest.GameEntity.Position);
            Movement.RunTo(closest.GameEntity.Position);
        }
        else
        {
            Movement.LookAt(Owner.GameEntity.EntityType == EntityType.Enemy
                ? States.MoveDirection.Left
                : States.MoveDirection.Right);
            Movement.RunTo(Owner.GameEntity.EntityType == EntityType.Enemy
                ? States.MoveDirection.Left
                : States.MoveDirection.Right);
        }

        return ActionOffset;
    }
}