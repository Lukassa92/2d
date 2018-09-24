using System.Collections.Generic;

public class MeleeUnitAI : BaseAI
{
    public MeleeUnitAI(GameEntity owner, MovementBehaviour movementBehaviour)
        : base(owner, movementBehaviour)
    {
    }

    protected override List<BaseAIBehaviour> GetBehaviours()
    {
        return new List<BaseAIBehaviour>(new MovementAIBehaviour[]
        {
            new MeleeMovementAIBehaviour(MovementBehaviour, Owner),
            new MeeleAttackAI(MovementBehaviour, Owner)
        });
    }
}