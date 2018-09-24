using System.Collections.Generic;
using System.Linq;

public class MeleeMovementAIBehaviour : MovementAIBehaviour
{
    public MeleeMovementAIBehaviour(CharacterMovement movement, TargetEntity owner) : base(movement, owner)
    {
    }

    private IEnumerable<TargetEntity> GetRelevantTargets()
    {
        return EntitiesInView.Where(e => e.GameEntity.EntityType != Owner.GameEntity.EntityType);
    }

    public override void OnTick()
    {
        if (NextActionPossible())
        {
            var relevantTargets = GetRelevantTargets().ToList();
            if (EntitiesInView.Count > 0 && relevantTargets.Count == 0)
            {
                var closest = GetClosestTarget(relevantTargets);
                Movement.RunTo(closest.GameEntity.Position);
                DoAction();
            }
            else
            {
                Movement.RunTo(Owner.GameEntity.EntityType == EntityType.Enemy
                    ? States.MoveDirection.Left
                    : States.MoveDirection.Right);
                DoAction();
            }
        }
    }
}